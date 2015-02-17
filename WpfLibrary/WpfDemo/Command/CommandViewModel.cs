namespace WpfDemo.Command
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Thinknet.MVVM.Command;
    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// Test view model for showing commands.
    /// </summary>
    public class CommandViewModel : ViewModel
    {
        private readonly ICommandManager _commandManager;
        private readonly ITaskRelayCommand<IEnumerable<string>> _goCommand;
        private string _result;
        private bool _canGo;
        private bool _throwException;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandViewModel"/> class.
        /// </summary>
        /// <param name="commandMananger">
        /// The command mananger.
        /// </param>
        public CommandViewModel(ICommandManager commandMananger)
        {
            _commandManager = commandMananger;
            _goCommand = new TaskRelayCommand<IEnumerable<string>>(token => AsyncOperation(token), CanExecute, new CommandManagerWrapper());
            _result = string.Empty;
        }

        public ITaskRelayCommandBase GoCommand
        {
            get { return _goCommand; }
        }

        public ICommand CancelCommand
        {
            get { return _goCommand.CancelCommand; }
        }

        public string Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        public bool CanGo
        {
            get { return _canGo; }
            set
            {
                SetProperty(ref _canGo, value);
                _commandManager.InvalidateRequerySuggested();
            }
        }

        public bool ThrowException
        {
            get { return _throwException; }
            set { _throwException = value; }
        }

        private async Task<IEnumerable<string>> AsyncOperation(CancellationToken token)
        {
            Result = string.Format("Clear in thread: {0}", Thread.CurrentThread.ManagedThreadId);

            IEnumerable<string> result = await Task.Factory.StartNew(() => DoSillyStuff(token), token);

            Result = string.Join(",", result) + string.Format(" Finished in thread: {0}", Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        private IEnumerable<string> DoSillyStuff(CancellationToken token)
        {
            Debug.WriteLine("Doing stuff in thread: {0}", Thread.CurrentThread.ManagedThreadId);
            List<string> data = new List<string>();
            int cnt = 0;
            while (!token.IsCancellationRequested && cnt < 100)
            {
                data.Add(cnt.ToString()); 
                Thread.Sleep(50);
                cnt++;
            }

            if (_throwException)
            {
                throw new ArgumentException("The property ShowException was true :-)");
            }

            return data;
        }

        private bool CanExecute()
        {
            return _canGo;
        }
    }
}
