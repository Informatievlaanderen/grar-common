namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Xunit
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Processing;

    internal class UnitTestWriter
    {
        private class Indent
        {
            private int _indents;
            private char _indenter;

            public Indent(char indenter = '\t')
            {
                _indenter = indenter;
                _indents = 0;
            }

            public static Indent operator ++(Indent current)
            {
                current._indents++;
                return current;
            }

            public static Indent operator --(Indent current)
            {
                current._indents--;
                return current;
            }

            public override string ToString()
            {
                return new string(Enumerable.Repeat(_indenter, _indents).ToArray());
            }
        }



        private readonly TextWriter _writer;
        private Indent _indent;
        private IUnitTestGeneratorConfig _config;
        private JsonSerializer _serializer;

        public UnitTestWriter(JsonSerializer serializer, TextWriter writer, IUnitTestGeneratorConfig config)
        {
            _serializer = serializer;
            _config = config;
            _writer = writer;
            _indent = new Indent();
        }

        public async Task WriteTest<TKey>(KeyImport<TKey> import)
        {
            var className = _config.GetClassName(import.Key);
            var createIdStatement = _config.GetCreateIdStatement(import.Key);

            await OpenNamespace();
            await WriteUsings();
            await OpenClass(className);
            await WriteConstructor(className);
            await WriteTests(import.Commands.Length);
            await WriteTestDataClass(className, createIdStatement, import.Commands);
            await WriteTestDataProperty(className);
            await WriteTestScenarios(import.Commands.Length);
            await CloseBraces();
            await CloseBraces();
        }

        private async Task WriteTestDataProperty(string className)
        {
            await WriteLineAsync($"private {className}Data _{{get;}}");
        }

        private async Task WriteTestScenarios(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await WriteLineAsync($"public IEventCentricTestSpecificationBuilder Command{i:D3}()");
                await OpenBraces();
                await WriteLineAsync("return new AutoFixtureScenario(new Fixture())");
                _indent++;
                if (i == 0)
                    await WriteLineAsync(".GivenNone()");
                else
                    await WriteLineAsync($".Given(Command{i - 1:D3}())");
                await WriteLineAsync($".When(_.Command{i:D3})");
                await WriteLineAsync(".Then(_.Id,");
                await WriteLineAsync("//add thens here");
                _indent++;
                await WriteLineAsync($"_.Command{i:D3}.ToLegacyEvent());");
                _indent--;
                _indent--;
                await CloseBraces();
                await WriteLineAsync();
            }
        }

        private async Task WriteTests(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await WriteLineAsync("[Fact]");
                await WriteLineAsync($"public void Command{i:D3}Test()");
                await OpenBraces();
                await WriteLineAsync($"Assert(Command{i:D3}());");
                await CloseBraces();
                await WriteLineAsync();
            }
        }

        private async Task WriteTestDataClass(string className, string createIdStatement, IEnumerable<ImportCommand> commands)
        {
            await WriteLineAsync("/// <summary>");
            await WriteLineAsync($"///Genereated test, add thens to complete");
            await WriteLineAsync("/// </summary>");
            await WriteLineAsync($"private class {className}Data");
            await OpenBraces();
            await WriteLineAsync($"public string Id => {createIdStatement};");
            await WriteLineAsync();
            int counter = 0;
            foreach (var command in commands)
            {
                await WriteLineAsync($"public {command.Type} Command{counter:D3} =>");
                _indent++;
                await WriteLineAsync($"JsonConvert.DeserializeObject<{command.Type}>(");
                _indent++;
                await WriteLineAsync($"{_serializer.Serialize(command.CrabItem)});");
                _indent--;
                _indent--;
                counter++;
            }
            await CloseBraces();
        }

        private async Task WriteConstructor(string className)
        {
            await WriteLineAsync($"public {className}(ITestOutputHelper testOutputHelper) : base(testOutputHelper)");
            await OpenBraces();
            await WriteLineAsync($"_ = new {className}Data();");
            await WriteLineAsync("JsonConvert.DefaultSettings =");
            _indent++;
            await WriteLineAsync("() => JsonSerializerSettingsProvider.CreateSerializerSettings().ConfigureDefaultForApi(); ");
            _indent--;
            await CloseBraces();
            await WriteLineAsync();
        }

        private async Task WriteUsings()
        {//TODO: make using configurable
            await WriteLineAsync("using Xunit;");
            await WriteLineAsync("using Xunit.Abstractions;");
            await WriteLineAsync("using Newtonsoft.Json;");
            await WriteLineAsync("using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;");
            await WriteLineAsync("using Be.Vlaanderen.Basisregisters.Crab;");
            await WriteLineAsync("using AutoFixture;");
            await WriteLineAsync("using global::AutoFixture;");
            await WriteLineAsync("using Microsoft.AspNetCore.Mvc.Formatters;");
            await WriteLineAsync("using Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Formatters.Json;");
            await WriteLineAsync();
        }

        private async Task OpenNamespace()
        {
            await WriteLineAsync($"namespace {_config.NamespaceName}");
            await OpenBraces();
        }

        private async Task OpenClass(string className)
        {
            await WriteLineAsync($"public class {className}:{_config.BaseClassName}");
            await OpenBraces();
        }

        private async Task OpenBraces()
        {
            await WriteLineAsync("{");
            _indent++;
        }

        private async Task CloseBraces(string textAfterBrace = null)
        {
            _indent--;
            await WriteLineAsync("}" + textAfterBrace);
        }

        private async Task WriteLineAsync()
        {
            await _writer.WriteLineAsync();
        }

        private async Task WriteLineAsync(string text)
        {
            await _writer.WriteLineAsync($"{_indent}{text}");
        }
    }
}
