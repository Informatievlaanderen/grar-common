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
            Func<ICommandProcessorOptions<int>> createProcessorOption = () => _sut.CreateProcessorOptions(null, new TestBatchConfiguration<int>());
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

    public class When_creating_init_processor_options_from_import_options
    {
        private readonly InitArguments _initArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_init_processor_options_from_import_options()
        {
            var fixture = new Fixture();

            _initArguments = fixture.Create<InitArguments>();
            _batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _initArguments.ToArguments(),
                    errors => {})
                .CreateProcessorOptions(
                    new ImportBatchStatus
                    {
                        From = fixture.Create<DateTime>(),
                        Until = fixture.Create<DateTime>(),
                        Completed = false,
                    },
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

    public class When_creating_init_processor_options_with_no_last_import_data
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly Func<DateTime> _getCurrentTimeStamp;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_init_processor_options_with_no_last_import_data()
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
                .CreateProcessorOptions(null, _batchConfiguration); 
        }

        [Fact]
        public void Then_from_should_be_default_from()
        {
            _createdOptions.From.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void Then_until_should_be_default_now_minus_the_configured_margin()
        {
            _createdOptions.Until.Should().Be(_getCurrentTimeStamp().Add(- _batchConfiguration.TimeMargin));
        }
    }

    public class When_creating_init_processor_options_for_an_invalid_last_import
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly Func<DateTime> _getCurrentTimeStamp;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_init_processor_options_for_an_invalid_last_import()
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
                .CreateProcessorOptions(new ImportBatchStatus
                {
                    From = fixture.Create<DateTime>(),
                    Until = default,
                    Completed = false
                }, _batchConfiguration); 
        }

        [Fact]
        public void Then_from_should_be_default_from()
        {
            _createdOptions.From.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void Then_until_should_be_default_now_minus_the_configured_margin()
        {
            _createdOptions.Until.Should().Be(_getCurrentTimeStamp().Add(- _batchConfiguration.TimeMargin));
        }
    }
    
    public class When_creating_init_processor_options_for_a_not_completed_last_import
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ImportBatchStatus _lastBatch;

        public When_creating_init_processor_options_for_a_not_completed_last_import()
        {
            var fixture = new Fixture();
            var fixedDateTimeNow = fixture.Create<DateTime>();

            var initArguments = fixture.Create<InitArguments>();
            _lastBatch = new ImportBatchStatus
            {
                From = fixture.Create<DateTime>(),
                Until = fixture.Create<DateTime>(),
                Completed = false
            };

            _createdOptions = new ImportOptions(
                    initArguments.ToArguments(),
                    errors => { },
                    () => fixedDateTimeNow)
                .CreateProcessorOptions(
                    _lastBatch,
                    new TestBatchConfiguration<int>(fixture.Create<TimeSpan>(), s => s.GetHashCode()));
        }

        [Fact]
        public void Then_from_should_be_last_batch_from()
        {
            _createdOptions.From.Should().Be(_lastBatch.From);
        }

        [Fact]
        public void Then_until_should_be_last_batch_until()
        {
            _createdOptions.Until.Should().Be(_lastBatch.Until);
        }
    }

    public class When_creating_init_processor_options_for_a_completed_last_import
    {
        private readonly Func<ICommandProcessorOptions<int>> _createOptions;

        public When_creating_init_processor_options_for_a_completed_last_import()
        {
            var fixture = new Fixture();
            var fixedDateTimeNow = fixture.Create<DateTime>();

            var initArguments = fixture.Create<InitArguments>();

            _createOptions = () => new ImportOptions(
                    initArguments.ToArguments(),
                    errors => { },
                    () => fixedDateTimeNow)
                .CreateProcessorOptions(
                    new ImportBatchStatus
                    {
                        From = fixture.Create<DateTime>(),
                        Until = fixture.Create<DateTime>(),
                        Completed = true
                    },
                    new TestBatchConfiguration<int>(fixture.Create<TimeSpan>(), s => s.GetHashCode()));
        }

        [Fact]
        public void Then_an_cannot_init_create_init_options_exception_should_be_thrown()
        {
            _createOptions
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Cannot initialize an for an already initialized import");
        }
    }

    public class When_creating_update_processor_options
    {
        private readonly UpdateArguments _updateArguments;
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_update_processor_options()
        {
            var fixture = new Fixture();

            _updateArguments = fixture.Create<UpdateArguments>();
            _batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    fixture.Create<ImportBatchStatus>(),
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

    public class When_creating_update_processor_options_with_no_last_import_data
    {
        private readonly Func<ICommandProcessorOptions<int>> _createOptions;

        public When_creating_update_processor_options_with_no_last_import_data()
        {
            var fixture = new Fixture();

            _createOptions = () =>
            {
                return new ImportOptions(
                        fixture.Create<UpdateArguments>().ToArguments(),
                        errors => { })
                    .CreateProcessorOptions(
                        null,
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

    public class When_creating_update_processor_options_for_an_invalid_last_import
    {
        private readonly Func<ICommandProcessorOptions<int>> _createOptions;

        public When_creating_update_processor_options_for_an_invalid_last_import()
        {
            var fixture = new Fixture();

            _createOptions = () =>
            {
                return new ImportOptions(
                        fixture.Create<UpdateArguments>().ToArguments(),
                        errors => { })
                    .CreateProcessorOptions(
                        new ImportBatchStatus
                        {
                            From = fixture.Create<DateTime>(),
                            Until = default,
                            Completed = fixture.Create<bool>()
                        }, 
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

    public class When_creating_update_processor_options_for_a_completed_previous_batch
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ImportBatchStatus _lastBatch;

        public When_creating_update_processor_options_for_a_completed_previous_batch()
        {
            var fixture = new Fixture();

            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _lastBatch = new ImportBatchStatus
            {
                From = fixture.Create<DateTime>(),
                Until = fixture.Create<DateTime>(),
                Completed = true
            };

            _createdOptions = new ImportOptions(
                    fixture.Create<UpdateArguments>().ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    _lastBatch, 
                    batchConfiguration);
        }

        [Fact]
        public void Then_from_should_be_the_last_batch_until()
        {
            _createdOptions.From.Should().Be(_lastBatch.Until);
        }

        [Fact]
        public void Then_clear_start_should_be_true()
        {
            _createdOptions.CleanStart.Should().BeTrue();
        }
    }

    public class When_creating_update_processor_options_for_a_completed_previous_batch_and_no_clean_start_argument
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;

        public When_creating_update_processor_options_for_a_completed_previous_batch_and_no_clean_start_argument()
        {
            var fixture = new Fixture();

            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            var updateArguments = fixture.Create<UpdateArguments>();
            updateArguments.CleanStart = false;

            _createdOptions = new ImportOptions(
                    updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    new ImportBatchStatus
                    {
                        From = fixture.Create<DateTime>(),
                        Until = fixture.Create<DateTime>(),
                        Completed = true
                    }, 
                    batchConfiguration);
        }

        [Fact]
        public void Then_clear_start_should_be_true()
        {
            _createdOptions.CleanStart.Should().BeTrue();
        }
    }

    public class When_creating_update_processor_options_for_a_completed_previous_batch_and_until_argument
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ImportBatchStatus _lastBatch;
        private UpdateArguments _updateArguments;

        public When_creating_update_processor_options_for_a_completed_previous_batch_and_until_argument()
        {
            var fixture = new Fixture();

            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _lastBatch = new ImportBatchStatus
            {
                From = fixture.Create<DateTime>(),
                Until = fixture.Create<DateTime>(),
                Completed = true
            };

            _updateArguments = fixture.Create<UpdateArguments>();
            _updateArguments.Until = fixture.Create<DateTime>();

            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    _lastBatch, 
                    batchConfiguration);
        }

        [Fact]
        public void Then_until_should_be_the_until_import_argument()
        {
            var updateArgumentsUntil = _updateArguments.Until ?? throw new Exception($"Setup went wrong, {nameof(UpdateArguments)}.{nameof(UpdateArguments.Until)} is empty");
            _createdOptions.Until.Should().BeCloseTo(updateArgumentsUntil, TimeSpan.FromMilliseconds(1));
        }
    }

    public class When_creating_update_processor_options_for_a_completed_previous_batch_and_no_until_argument
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly Func<DateTime> _getCurrentTimeStamp;
        private readonly ICommandProcessorBatchConfiguration<int> _batchConfiguration;

        public When_creating_update_processor_options_for_a_completed_previous_batch_and_no_until_argument()
        {
            var fixture = new Fixture();
            var fixedDateTimeNow = fixture.Create<DateTime>();

            var updateArguments = fixture.Create<UpdateArguments>();
            updateArguments.Until = null;

            _getCurrentTimeStamp = () => fixedDateTimeNow;
            _batchConfiguration = new TestBatchConfiguration<int>(fixture.Create<TimeSpan>(), s => s.GetHashCode());

            _createdOptions = new ImportOptions(
                    updateArguments.ToArguments(),
                    errors => { },
                    _getCurrentTimeStamp)
                .CreateProcessorOptions(
                    new ImportBatchStatus
                    {
                        From = fixture.Create<DateTime>(),
                        Until = fixture.Create<DateTime>(),
                        Completed = true
                    }, 
                    _batchConfiguration);
        }
        
        [Fact]
        public void Then_until_should_be_the_default_until_date()
        {
            _createdOptions.Until.Should().Be(_getCurrentTimeStamp().Add(-_batchConfiguration.TimeMargin));
        }
    }

    public class When_creating_update_processor_options_for_an_not_completed_previous_batch
    {
        private readonly ICommandProcessorOptions<int> _createdOptions;
        private readonly ImportBatchStatus _lastBatch;
        private readonly UpdateArguments _updateArguments;

        public When_creating_update_processor_options_for_an_not_completed_previous_batch()
        {
            var fixture = new Fixture();

            var batchConfiguration = new TestBatchConfiguration<int>(s => s.GetHashCode());

            _lastBatch = new ImportBatchStatus
            {
                From = fixture.Create<DateTime>(),
                Until = fixture.Create<DateTime>(),
                Completed = false
            };

            _updateArguments = fixture.Create<UpdateArguments>();
            _createdOptions = new ImportOptions(
                    _updateArguments.ToArguments(),
                    errors => { })
                .CreateProcessorOptions(
                    _lastBatch,
                    batchConfiguration);
        }

        [Fact]
        public void Then_from_should_be_the_last_batch_from()
        {
            _createdOptions.From.Should().Be(_lastBatch.From);
        }

        [Fact]
        public void Then_until_should_be_the_last_batch_until()
        {
            _createdOptions.Until.Should().Be(_lastBatch.Until);
        }

        [Fact]
        public void Then_clear_start_should_be_arguments_clean_start()
        {
            _createdOptions.CleanStart.Should().Be(_updateArguments.CleanStart);
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
            TimeMargin = margin;
        }

        public TimeSpan TimeMargin { get; }
        public TKey Deserialize(string key) =>
            null != _deserialize
                ? _deserialize(key)
                : throw new NotImplementedException($"{GetType().Name}.{nameof(Deserialize)}");
    }

    public static class ArgumentExtensions
    {
        private static IEnumerable<string> ToArguments(this InitArguments init)
        {
            var arguments = new List<string> { "init" };

            if (init.Take.HasValue)
                arguments.AddRange(new []{ "-t", init.Take.ToString() });

            return arguments;
        }

        private static IEnumerable<string> ToArguments(this UpdateArguments update)
        {
            var arguments = new List<string> { "update" };

            if (update.Until.HasValue)
                arguments.AddRange(new[] { "--until", update.Until.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") });

            return arguments;
        }

        public static IEnumerable<string> ToArguments<TArguments>(this TArguments importArguments)
            where TArguments : ImportArguments
        {
            var args = new List<string>();

            switch (importArguments)
            {
                case InitArguments init:
                    args.AddRange(init.ToArguments());
                    break;
                case UpdateArguments update:
                    args.AddRange(update.ToArguments());
                    break;
            }

            args.AddRange(new []{ "-l", importArguments.LogLevel.ToString() });

            if (importArguments.Keys?.Any() ?? false)
                args.AddRange(new []{ "-k", string.Join(',', importArguments.Keys) });

            if (importArguments.DryRun)
                args.Add("-d");

            if (importArguments.CleanStart)
                args.Add("-c");

            return args;
        }

        public static ImportOptions ToImportOptions<TArguments>(this TArguments arguments)
            where TArguments : ImportArguments
            => arguments.ToImportOptions(null);

        public static ImportOptions ToImportOptions<TArguments>(this TArguments arguments, Action<IEnumerable<Error>> onNotParsed)
            where TArguments : ImportArguments
            => new ImportOptions(arguments.ToArguments(), onNotParsed ?? (errors => {}));
    }
}
