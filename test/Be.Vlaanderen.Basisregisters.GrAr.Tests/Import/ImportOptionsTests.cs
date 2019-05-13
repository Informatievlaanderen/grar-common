namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;
    using GrAr.Import.Processing;
    using Moq;
    using Xunit;
    using FluentAssertions;
    using GrAr.Import.Processing.CommandLine;
    using AutoFixture;

    public class When_creating_import_options_for_an_undefined_argument_type
    {
        private readonly Mock<Action<IEnumerable<Error>>> _onNotParsedMock;
        private readonly ImportOptions _sut;

        public When_creating_import_options_for_an_undefined_argument_type()
        {
            _onNotParsedMock = new Mock<Action<IEnumerable<Error>>>();
            _sut = new ImportOptions(new List<string>(), _onNotParsedMock.Object);
        }
        
        [Fact]
        public void Then_on_not_parsed_is_called()
        {
            _onNotParsedMock.Verify(
                onNotParse => onNotParse(It.IsAny<IEnumerable<Error>>()),
                Times.Once);
        }

        [Fact]
        public void Then_import_arguments_throws_an_exception()
        {
            Func<ImportArguments> getImportArguments = () => _sut.ImportArguments;
            getImportArguments
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("No valid command line arguments");
        }

        [Fact]
        public void Then_create_processor_options_throws_an_exception()
        {
            Func<ICommandProcessorOptions<int>> createProcessorOption = () => _sut.CreateProcessorOptions(null, null, new TestBatchConfiguration<int>());
            createProcessorOption
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("No valid command line arguments");
        }
    }

    public class When_creating_import_options_for_init
    {
        private readonly Mock<Action<IEnumerable<Error>>> _onNotParsedMock;
        private readonly ImportOptions _sut;
        private readonly InitArguments _expectedArguments;

        public When_creating_import_options_for_init()
        {
            _expectedArguments = new Fixture().Create<InitArguments>();
            _onNotParsedMock = new Mock<Action<IEnumerable<Error>>>();

            _sut = new ImportOptions(_expectedArguments.ToArguments(), _onNotParsedMock.Object);
        }
        
        [Fact]
        public void Then_on_not_parsed_is_not_called()
        {
            _onNotParsedMock.Verify(
                onNotParse => onNotParse(It.IsAny<IEnumerable<Error>>()),
                Times.Never);
        }

        [Fact]
        public void Then_import_arguments_should_contain_the_given_arguments()
        {
            _sut.ImportArguments
                .Should()
                .BeEquivalentTo((ImportArguments)_expectedArguments);
        }
    }

    public class When_creating_import_options_for_update
    {
        private readonly Mock<Action<IEnumerable<Error>>> _onNotParsedMock;
        private readonly ImportOptions _sut;
        private readonly UpdateArguments _expectedArguments;

        public When_creating_import_options_for_update()
        {
            _expectedArguments = new Fixture().Create<UpdateArguments>();
            _onNotParsedMock = new Mock<Action<IEnumerable<Error>>>();

            _sut = new ImportOptions(_expectedArguments.ToArguments(), _onNotParsedMock.Object);
        }
        
        [Fact]
        public void Then_on_not_parsed_is_not_called()
        {
            _onNotParsedMock.Verify(
                onNotParse => onNotParse(It.IsAny<IEnumerable<Error>>()),
                Times.Never);
        }

        [Fact]
        public void Then_import_arguments_should_contain_the_given_arguments()
        {
            _sut.ImportArguments
                .Should()
                .BeEquivalentTo((ImportArguments)_expectedArguments);
        }
    }

    public class When_creating_init_options
    {
        private readonly InitArguments _initArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_init_options()
        {
            var fixture = new Fixture();

            _initArguments = fixture.Create<InitArguments>();
            _batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _initArguments.ToArguments(),
                    errors => {})
                .CreateProcessorOptions(
                    fixture.Create<DateTime?>(),
                    fixture.Create<DateTime?>(),
                    _batchConfiguration); 
        }

        [Fact]
        public void Then_import_mode_should_be_init()
        {
            _createdOptions.Mode.Should().Be(ImportMode.Init);
        }

        [Fact]
        public void Then_clean_start_should_be_arguments_clean_start()
        {
            _createdOptions.CleanStart.Should().Be(_initArguments.CleanStart);
        }

        [Fact]
        public void Then_take_should_be_the_arguments_take()
        {
            _createdOptions.Take.Should().Be(_initArguments.Take);
        }

        [Fact]
        public void Then_keys_should_be_the_arguments_deserialized_keys()
        {
            _createdOptions.Keys.Should().BeEquivalentTo(_initArguments.Keys.Select(_batchConfiguration.Deserialize));
        }
    }

    public class When_creating_init_with_no_previous_import_data
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly Func<DateTime> _getCurrentTimeStamp;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_init_with_no_previous_import_data()
        {
            var fixture = new Fixture();
            var fixedDateTimeNow = fixture.Create<DateTime>();

            var initArguments = fixture.Create<InitArguments>();
            _getCurrentTimeStamp = () => fixedDateTimeNow;
            _batchConfiguration = new TestBatchConfiguration<int>(fixture.Create<TimeSpan>(), s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    initArguments.ToArguments(),
                    errors => {},
                    _getCurrentTimeStamp)
                .CreateProcessorOptions(
                    null,
                    null,
                    _batchConfiguration); 
        }

        [Fact]
        public void Then_from_should_be_default_from()
        {
            _createdOptions.From.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void Then_until_should_be_default_now_minus_the_configured_margin()
        {
            _createdOptions.Until.Should().Be(_getCurrentTimeStamp().Add(- _batchConfiguration.Margin));
        }
    }

    public class When_creating_init_with_a_given_last_completed_import_date
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly DateTime _lastCompletedImport;

        public When_creating_init_with_a_given_last_completed_import_date()
        {
            var fixture = new Fixture();

            var initArguments = fixture.Create<InitArguments>();
            _lastCompletedImport = fixture.Create<DateTime>();
            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    initArguments.ToArguments(),
                    errors => {})
                .CreateProcessorOptions(
                    _lastCompletedImport,
                    fixture.Create<DateTime?>(),
                    batchConfiguration); 
        }

        [Fact]
        public void Then_from_should_be_the_last_completed_import_date()
        {
            _createdOptions.From.Should().Be(_lastCompletedImport);
        }
    }

    public class When_creating_init_with_a_given_recover_until_date
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly DateTime _recoverUntil;

        public When_creating_init_with_a_given_recover_until_date()
        {
            var fixture = new Fixture();

            var initArguments = fixture.Create<InitArguments>();
            _recoverUntil = fixture.Create<DateTime>();
            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    initArguments.ToArguments(),
                    errors => {})
                .CreateProcessorOptions(
                    fixture.Create<DateTime?>(),
                    _recoverUntil,
                    batchConfiguration); 
        }

        [Fact]
        public void Then_until_should_be_the_recover_until_date()
        {
            _createdOptions.Until.Should().Be(_recoverUntil);
        }
    }

    public class When_creating_update_options
    {
        private readonly UpdateArguments _updateArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_update_options()
        {
            var fixture = new Fixture();

            _updateArguments = fixture.Create<UpdateArguments>();
            _batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    fixture.Create<DateTime>(),
                    fixture.Create<DateTime?>(),
                    _batchConfiguration);
        }

        [Fact]
        public void Then_import_mode_should_be_update()
        {
            _createdOptions.Mode.Should().Be(ImportMode.Update);
        }

        [Fact]
        public void Then_take_should_be_null()
        {
            _createdOptions.Take.Should().BeNull();
        }

        [Fact]
        public void Then_keys_should_be_the_arguments_deserialized_keys()
        {
            _createdOptions.Keys.Should().BeEquivalentTo(_updateArguments.Keys.Select(_batchConfiguration.Deserialize));
        }
    }

    public class When_creating_update_options_with_no_last_completed_import_date
    {
        private readonly Func<ICommandProcessorOptions<int>> _createOptions;

        public When_creating_update_options_with_no_last_completed_import_date()
        {
            var fixture = new Fixture();

            _createOptions = () =>
            {
                return new ImportOptions(
                        fixture.Create<UpdateArguments>().ToArguments(),
                        errors => { })
                    .CreateProcessorOptions(
                        null,
                        fixture.Create<DateTime?>(),
                        new TestBatchConfiguration<int>());
            };
        }

        [Fact]
        public void Then_create_options_should_throw_an_exception()
        {
            _createOptions
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Cannot update an uninitialized import");
        }
    }

    public class When_creating_update_options_with_a_last_completed_import_date
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly DateTime _lastCompletedImport;

        public When_creating_update_options_with_a_last_completed_import_date()
        {
            var fixture = new Fixture();

            _lastCompletedImport = fixture.Create<DateTime>();
            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    fixture.Create<UpdateArguments>().ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    _lastCompletedImport,
                    fixture.Create<DateTime?>(),
                    batchConfiguration);
        }

        [Fact]
        public void Then_from_should_be_the_last_completed_import_date()
        {
            _createdOptions.From.Should().Be(_lastCompletedImport);
        }
    }

    public class When_creating_update_options_with_a_recover_until_date
    {
        private readonly UpdateArguments _updateArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly DateTime _recoverUntil;

        public When_creating_update_options_with_a_recover_until_date()
        {
            var fixture = new Fixture();

            _updateArguments = fixture.Create<UpdateArguments>();
            _recoverUntil = fixture.Create<DateTime>();
            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    fixture.Create<DateTime>(),
                    _recoverUntil,
                    batchConfiguration);
        }

        [Fact]
        public void Then_until_should_be_the_recover_until_date()
        {
            _createdOptions.Until.Should().Be(_recoverUntil);
        }

        [Fact]
        public void Then_clear_should_be_the_argument_clear_start()
        {
            _createdOptions.CleanStart.Should().Be(_updateArguments.CleanStart);
        }
    }
    
    public class When_creating_update_options_with_no_recover_until_date_and_an_until_argument
    {
        private readonly UpdateArguments _updateArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;

        public When_creating_update_options_with_no_recover_until_date_and_an_until_argument()
        {
            var fixture = new Fixture();

            _updateArguments = fixture.Create<UpdateArguments>();
            _updateArguments.Until = fixture.Create<DateTime>();
            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    fixture.Create<DateTime>(),
                    null,
                    batchConfiguration);
        }

        [Fact]
        public void Then_until_should_be_the_argument_until_date()
        {
            var until = _updateArguments.Until ?? throw new Exception($"Setup went wrong, {nameof(UpdateArguments)}.{nameof(UpdateArguments.Until)} is empty");
            _createdOptions.Until.Should().BeCloseTo(until, TimeSpan.FromMilliseconds(1));
        }

        [Fact]
        public void Then_clear_start_should_be_true()
        {
            _createdOptions.CleanStart.Should().Be(true);
        }
    }
    
    public class When_creating_update_options_with_no_recover_until_date_and_no_until_argument
    {
        private readonly UpdateArguments _updateArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly Func<DateTime> _getCurrentTimeStamp;
        private TestBatchConfiguration<int> _batchConfiguration;

        public When_creating_update_options_with_no_recover_until_date_and_no_until_argument()
        {
            var fixture = new Fixture();
            var fixedDateTimeNow = fixture.Create<DateTime>();

            _updateArguments = fixture.Create<UpdateArguments>();
            _updateArguments.Until = null;

            _getCurrentTimeStamp = () => fixedDateTimeNow;
            _batchConfiguration = new TestBatchConfiguration<int>(fixture.Create<TimeSpan>(), s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { },
                    _getCurrentTimeStamp)
                .CreateProcessorOptions(
                    fixture.Create<DateTime>(),
                    null,
                    _batchConfiguration);
        }

        [Fact]
        public void Then_until_should_be_the_default_until_date()
        {
            _createdOptions.Until.Should().Be(_getCurrentTimeStamp().Add(-_batchConfiguration.Margin));
        }

        [Fact]
        public void Then_clear_start_should_be_true()
        {
            _createdOptions.CleanStart.Should().Be(true);
        }
    }

    public class TestBatchConfiguration<TKey> : ICommandProcessorBatchConfiguration<TKey>
    {
        private readonly Func<string, TKey> _deserialize;

        public TestBatchConfiguration()
            : this(TimeSpan.Zero, null)
        { }
        public TestBatchConfiguration(Func<string, TKey> deserialize)
            : this(TimeSpan.Zero, deserialize)
        { }

        public TestBatchConfiguration(TimeSpan margin)
            : this(margin, null)
        { }

        public TestBatchConfiguration(TimeSpan margin, Func<string, TKey> deserialize)
        {
            _deserialize = deserialize;
            Margin = margin;
        }

        public TimeSpan Margin { get; }
        public TKey Deserialize(string key) =>
            null != _deserialize
                ? _deserialize(key)
                : throw new NotImplementedException();
    }

    public static class ArgumentExtensions
    {
        public static IEnumerable<string> ToArguments(this InitArguments init)
        {
            var arguments = new List<string> { "init" };

            arguments.AddRange(((ImportArguments)init).ToArguments());

            if (init.Take.HasValue)
                arguments.AddRange(new []{ "-t", init.Take.ToString() });

            return arguments;
        }
        public static IEnumerable<string> ToArguments(this UpdateArguments update)
        {
            var arguments = new List<string> { "update" };

            arguments.AddRange(((ImportArguments)update).ToArguments());

            if (update.Until.HasValue)
                arguments.AddRange(new[] { "--until", update.Until.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") });

            return arguments;
        }

        private static IEnumerable<string> ToArguments(this ImportArguments args)
        {
            return new List<string>
            {
                "-l", args.LogLevel.ToString(),
                args.DryRun ? "-d" : "",
                "-k", string.Join(',', args.Keys),
                args.CleanStart ? "-c" : ""
            };
        }
    }
}
