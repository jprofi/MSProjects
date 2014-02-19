namespace Thinknet.MVVM.Test.Command
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Input;

    using NUnit.Framework;

    using Thinknet.MVVM.Command;

    /// <summary>
    ///     Tests for the MVVM Command classes.
    /// </summary>
    [TestFixture]
    public class RelayCommandTest
    {
        [Test]
        public void TestAsyncRelayCommandNoParam()
        {
            const int processDuration = 500;

            AutoResetEvent ev = new AutoResetEvent(false);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool asyncActionPerformed = false;
            bool actionFinished = false;
            bool canExecutePerformed = false;

            ICommand command = new AsyncRelayCommand(
                // ReSharper disable ImplicitlyCapturedClosure
                () =>
                    {
                        Thread.Sleep(processDuration);
                        asyncActionPerformed = true;
                    }, 
                () =>
                    {
                        Assert.That(asyncActionPerformed, Is.True, "Async method call should be called before finished action.");
                        actionFinished = true;
                        ev.Set();
                    }, 
                () =>
                    {
                        canExecutePerformed = true;
                        return true;
                    }
                // ReSharper restore ImplicitlyCapturedClosure
            );

            Assert.That(command.CanExecute(null), Is.True);
            command.Execute(null);
            Assert.That(command.CanExecute(null), Is.False);
            Assert.That(stopWatch.ElapsedMilliseconds, Is.LessThan(processDuration));

            bool exitBySignal = ev.WaitOne(2000);
            Assert.That(exitBySignal, Is.True, "Timeout by waiting for command completion.");

            Assert.That(asyncActionPerformed, Is.True);
            Assert.That(actionFinished, Is.True);
            Assert.That(canExecutePerformed, Is.True);
        }

        [Test]
        public void TestAsyncRelayCommandNoParamNoFinishedAction()
        {
            const int processDuration = 500;

            AutoResetEvent ev = new AutoResetEvent(false);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool asyncActionPerformed = false;
            bool actionFinished = false;
            bool canExecutePerformed = false;

            ICommand command = new AsyncRelayCommand(
                // ReSharper disable ImplicitlyCapturedClosure
                () =>
                {
                    Thread.Sleep(processDuration);
                    asyncActionPerformed = true;
                    actionFinished = true;
                    ev.Set();
                },
                () =>
                {
                    canExecutePerformed = true;
                    return true;
                }
                // ReSharper restore ImplicitlyCapturedClosure
            );

            Assert.That(command.CanExecute(null), Is.True);
            command.Execute(null);
            Assert.That(command.CanExecute(null), Is.False);
            Assert.That(stopWatch.ElapsedMilliseconds, Is.LessThan(processDuration));

            bool exitBySignal = ev.WaitOne(2000);
            Assert.That(exitBySignal, Is.True, "Timeout by waiting for command completion.");

            Assert.That(asyncActionPerformed, Is.True);
            Assert.That(actionFinished, Is.True);
            Assert.That(canExecutePerformed, Is.True);
        }

        [Test]
        public void TestAsyncRelayCommandWithParam()
        {
            const int processDuration = 500;
            const string parameterString = "123";
            int parameterAsInt = int.Parse(parameterString);

            AutoResetEvent ev = new AutoResetEvent(false);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool asyncActionPerformed = false;
            bool actionFinished = false;
            bool canExecutePerformed = false;

            string executeActionParam = string.Empty;
            string executeFinishedParam = string.Empty;

            AsyncRelayCommand<string> command = new AsyncRelayCommand<string>(
                // ReSharper disable ImplicitlyCapturedClosure
                e =>
                    {
                        executeActionParam = e;
                        Thread.Sleep(processDuration);
                        asyncActionPerformed = true;
                    }, 
                e =>
                    {
                        Assert.That(asyncActionPerformed, Is.True, "Async method call should be called before finished action.");
                        executeFinishedParam = e;
                        actionFinished = true;
                        ev.Set();
                    }, 
                e =>
                    {
                        canExecutePerformed = true;
                        Assert.That(e, Is.EqualTo(parameterString));
                        return true;
                    }

                // ReSharper restore ImplicitlyCapturedClosure
            );

            Assert.That(command.CanExecute(parameterString), Is.True);
            Assert.That(command.CanExecute(parameterAsInt), Is.True);
            command.Execute(parameterString);
            Assert.That(command.CanExecute(parameterString), Is.False);
            Assert.That(stopWatch.ElapsedMilliseconds, Is.LessThan(processDuration));

            bool exitBySignal = ev.WaitOne(2000);
            Assert.That(exitBySignal, Is.True, "Timeout by waiting for command completion.");

            // Check convertible parameter.
            Assert.That(asyncActionPerformed, Is.True);
            Assert.That(actionFinished, Is.True);
            Assert.That(canExecutePerformed, Is.True);

            Assert.That(executeActionParam, Is.EqualTo(parameterString));
            Assert.That(executeFinishedParam, Is.EqualTo(parameterString));
        }

        [Test]
        public void TestAsyncRelayCommandWithParamNoFinishedaction()
        {
            const int processDuration = 500;
            const string parameterString = "123";
            int parameterAsInt = int.Parse(parameterString);

            AutoResetEvent ev = new AutoResetEvent(false);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool asyncActionPerformed = false;
            bool actionFinished = false;
            bool canExecutePerformed = false;

            string executeActionParam = string.Empty;
            string executeFinishedParam = string.Empty;

            AsyncRelayCommand<string> command = new AsyncRelayCommand<string>(
                // ReSharper disable ImplicitlyCapturedClosure
                e =>
                    {
                        executeActionParam = e;
                        Thread.Sleep(processDuration);
                        asyncActionPerformed = true;
                        executeFinishedParam = e;
                        actionFinished = true;
                        ev.Set();
                    }, 
                e =>
                    {
                        canExecutePerformed = true;
                        Assert.That(e, Is.EqualTo(parameterString));
                        return true;
                    }

                // ReSharper restore ImplicitlyCapturedClosure
            );

            Assert.That(command.CanExecute(parameterString), Is.True);
            Assert.That(command.CanExecute(parameterAsInt), Is.True);
            command.Execute(parameterString);
            Assert.That(command.CanExecute(parameterString), Is.False);
            Assert.That(stopWatch.ElapsedMilliseconds, Is.LessThan(processDuration));

            bool exitBySignal = ev.WaitOne(2000);
            Assert.That(exitBySignal, Is.True, "Timeout by waiting for command completion.");

            // Check convertible parameter.
            Assert.That(asyncActionPerformed, Is.True);
            Assert.That(actionFinished, Is.True);
            Assert.That(canExecutePerformed, Is.True);

            Assert.That(executeActionParam, Is.EqualTo(parameterString));
            Assert.That(executeFinishedParam, Is.EqualTo(parameterString));
        }

        [Test]
        public void TestCommandConstructorExceptions()
        {
            // ReSharper disable ObjectCreationAsStatement
            Assert.Catch<ArgumentNullException>(() => { new RelayCommand(null, () => true); });
            Assert.Catch<ArgumentNullException>(() => { new RelayCommand(null, null); });
            Assert.Catch<ArgumentNullException>(() => { new RelayCommand<string>(null, e => true); });
            Assert.Catch<ArgumentNullException>(() => { new RelayCommand<string>(null, null); });

            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void TestRelayCommandNoParam()
        {
            bool actionPerformed = false;
            bool canExecutePerformed = false;

            BaseCommand command = new RelayCommand(
                () => { actionPerformed = true; }, 
                () =>
                    {
                        canExecutePerformed = true;
                        return true;
                    });

            Assert.That(command.HasCanExecuteFunction, Is.True);
            Assert.That(command.CanExecute(null), Is.True);
            command.Execute(null);

            Assert.That(command.CanExecute(null), Is.True);
            Assert.That(actionPerformed, Is.True);
            Assert.That(canExecutePerformed, Is.True);
        }

        [Test]
        public void TestRelayCommandNoParamNoCanExecute()
        {
            bool actionPerformed = false;

            BaseCommand command = new RelayCommand(
                () => { actionPerformed = true; });

            Assert.That(command.HasCanExecuteFunction, Is.False);
            Assert.That(command.CanExecute(null), Is.True);
            command.Execute(null);

            Assert.That(command.CanExecute(null), Is.True);
            Assert.That(actionPerformed, Is.True);
        }

        [Test]
        public void TestRelayCommandWithNullValueTypeParam()
        {
            RelayCommand<int> command = new RelayCommand<int>(
                e => Assert.That(e, Is.EqualTo(0)), 
                i =>
                    {
                        Assert.That(i, Is.EqualTo(0));
                        return true;
                    });

            Assert.That(command.HasCanExecuteFunction, Is.True);
            Assert.That(command.CanExecute(null), Is.True);
            command.Execute(null);
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void TestRelayCommandWithParam()
        {
            const string parameterString = "234";
            int parameterAsInt = int.Parse(parameterString);

            bool actionPerformed = false;
            bool canExecutePerformed = false;

            string executeActionParam = string.Empty;

            RelayCommand<string> command = new RelayCommand<string>(
                e =>
                    {
                        executeActionParam = e;
                        actionPerformed = true;
                    }, 
                e =>
                    {
                        canExecutePerformed = true;
                        Assert.That(e, Is.EqualTo(parameterString));
                        return true;
                    });

            Assert.That(command.HasCanExecuteFunction, Is.True);
            Assert.That(command.CanExecute(parameterString), Is.True);
            command.Execute(parameterAsInt);
            Assert.That(command.CanExecute(parameterString), Is.True);

            // Check convertible parameter.
            Assert.That(command.CanExecute(parameterAsInt), Is.True);
            Assert.That(actionPerformed, Is.True);
            Assert.That(canExecutePerformed, Is.True);

            Assert.That(executeActionParam, Is.EqualTo(parameterString));
        }

        [Test]
        public void TestRelayCommandWithParamNoCanExecute()
        {
            const string parameterString = "567";
            int parameterAsInt = int.Parse(parameterString);

            bool actionPerformed = false;

            string executeActionParam = string.Empty;

            RelayCommand<string> command = new RelayCommand<string>(
                e =>
                    {
                        executeActionParam = e;
                        actionPerformed = true;
                    });

            Assert.That(command.HasCanExecuteFunction, Is.False);
            Assert.That(command.CanExecute(parameterString), Is.True);
            command.Execute(parameterAsInt);
            Assert.That(command.CanExecute(parameterString), Is.True);

            // Check convertible parameter.
            Assert.That(command.CanExecute(parameterAsInt), Is.True);
            Assert.That(actionPerformed, Is.True);

            Assert.That(executeActionParam, Is.EqualTo(parameterString));
        }
    }
}
