namespace Thinknet.MVVM.Test.ViewModel
{
    using System;
    using System.Linq.Expressions;

    using NUnit.Framework;

    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// The test view model.
    /// </summary>
    [TestFixture]
    public class ViewModelTest
    {
        private bool DummyProperty { get; set; }

        /// <summary>
        /// The test event handler.
        /// </summary>
        [Test]
        public void TestEventHandler()
        {
            // Create viewmodel
            DummyViewModel vm = new DummyViewModel();

            // Register event handler
            vm.PropertyChanged += (s, e) => Assert.AreEqual("DummyProperty", e.PropertyName);

            // Raise property changed event
            vm.RaisePropertyChanged(() => DummyProperty);
        }   
        
        /// <summary>
        /// The test event handler.
        /// </summary>
        [Test]
        public void TestEventHandlerCompiler()
        {
            // Create viewmodel
            DummyViewModel vm = new DummyViewModel();

            // Register event handler
            vm.PropertyChanged += (s, e) => Assert.AreEqual("Name", e.PropertyName);

            // Raise property changed event
            vm.Name = "Hello";
        }

        /// <summary>
        /// The test error handling.
        /// </summary>
        [Test]
        public void TestErrorHandling()
        {
            DummyViewModel vm = new DummyViewModel();
            Assert.That(vm["PropertyRaisingAnError"], Is.Null);

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorOne;
            Assert.That(vm["PropertyRaisingAnError"], Is.EqualTo("ErrorOne"));

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorTwo;
            Assert.That(vm["PropertyRaisingAnError"], Is.EqualTo("ErrorTwo\r\nErrorOne"));

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.RemoveErrorOne;
            Assert.That(vm["PropertyRaisingAnError"], Is.EqualTo("ErrorTwo"));

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorTwo;
            Assert.That(vm["PropertyRaisingAnError"], Is.EqualTo("ErrorTwo"));

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorOne;
            Assert.That(vm["PropertyRaisingAnError"], Is.EqualTo("ErrorOne\r\nErrorTwo"));

            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.RemoveAllErrors;
            Assert.That(vm["PropertyRaisingAnError"], Is.Null);
            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorOne;
            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.SetErrorTwo;
            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.RemoveErrorOne;
            vm.PropertyRaisingAnError = DummyViewModel.EnumHelper.Error.RemoveErrorTwo;
            Assert.That(vm["PropertyRaisingAnError"], Is.Null);
        }
    }

    /// <summary>
    /// The dummy view model.
    /// </summary>
    internal class DummyViewModel : ViewModel
    {
        private string _name = string.Empty;
        
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Creates or removes errors on this property.
        /// </summary>
        public EnumHelper.Error PropertyRaisingAnError
        {
            get { return EnumHelper.Error.Return; }

            set
            {
                switch (value)
                {
                    case EnumHelper.Error.RemoveAllErrors:
                        {
                            RemoveAllErrors(() => PropertyRaisingAnError);
                        }

                        break;
                    case EnumHelper.Error.SetErrorOne:
                        {
                            // AddError(() => PropertyRaisingAnError, "ErrorOne", false);
                            AddError("ErrorOne", false);
                        }

                        break;
                    case EnumHelper.Error.RemoveErrorOne:
                        {
                            RemoveError("ErrorOne");
                        }

                        break;
                    case EnumHelper.Error.SetErrorTwo:
                        {
                            AddError(() => PropertyRaisingAnError, "ErrorTwo", false);
                        }

                        break;
                    case EnumHelper.Error.RemoveErrorTwo:
                        {
                            RemoveError(() => PropertyRaisingAnError, "ErrorTwo");
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// The raise property changed.
        /// </summary>
        /// <param name="changedProperty">
        /// The changed property.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void RaisePropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            NotifyPropertyChanged(changedProperty);
        }

        /// <summary>
        /// The enum helper.
        /// </summary>
        public static class EnumHelper
        {
            /// <summary>
            /// The error.
            /// </summary>
            public enum Error
            {
                /// <summary>
                /// The remove all errors.
                /// </summary>
                RemoveAllErrors, 

                /// <summary>
                /// The return.
                /// </summary>
                Return, 

                /// <summary>
                /// The set error one.
                /// </summary>
                SetErrorOne, 

                /// <summary>
                /// The remove error one.
                /// </summary>
                RemoveErrorOne, 

                /// <summary>
                /// The set error two.
                /// </summary>
                SetErrorTwo, 

                /// <summary>
                /// The remove error two.
                /// </summary>
                RemoveErrorTwo
            }
        }
    }
}
