namespace Thinknet.MVVM.Test.Helper.TestHelperClasses
{
    using Thinknet.MVVM.Helper;

    internal class InternalTestClass<T>
    {
        public const string Expected = "Hello";
        public const string Internal = "Internal";
        public const string InternalStatic = "InternalStatic";
        public const string Private = "Private";
        public const string PrivateStatic = "PrivateStatic";
        public const string Public = "Public";
        public const string PublicStatic = "PublicStatic";
        private readonly int _index; // Just here to force instance methods

        public InternalTestClass()
        {
        }

        public InternalTestClass(int index)
        {
            _index = index;
        }

        public static string Result { get; private set; }

        public static void DoStuffPublicallyAndStatically(T parameter)
        {
            Result = Expected + PublicStatic + parameter;
        }

        public static string DoStuffPublicallyAndStaticallyWithResult(T parameter)
        {
            Result = Expected + PublicStatic + parameter;
            return Result;
        }

        public void DoStuffPublically(T parameter)
        {
            Result = Expected + Public + _index + parameter;
        }

        public string DoStuffPublicallyWithResult(T parameter)
        {
            Result = Expected + Public + _index + parameter;
            return Result;
        }

        public WeakAction<T> GetAction(WeakActionTestCase testCase)
        {
            WeakAction<T> action = null;

            switch (testCase)
            {
                case WeakActionTestCase.PublicNamedMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffPublically);
                    break;
                case WeakActionTestCase.InternalNamedMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffInternally);
                    break;
                case WeakActionTestCase.PrivateNamedMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffPrivately);
                    break;
                case WeakActionTestCase.PublicStaticMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffPublicallyAndStatically);
                    break;
                case WeakActionTestCase.PrivateStaticMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffPrivatelyAndStatically);
                    break;
                case WeakActionTestCase.InternalStaticMethod:
                    action = new WeakAction<T>(
                        this, 
                        DoStuffInternallyAndStatically);
                    break;
                case WeakActionTestCase.AnonymousStaticMethod:
                    action = new WeakAction<T>(
                        this, 
                        p => Result = Expected + p);
                    break;
                case WeakActionTestCase.AnonymousMethod:
                    action = new WeakAction<T>(
                        this, 
                        p => Result = Expected + _index + p);
                    break;
            }

            return action;
        }

        public WeakFunc<T, string> GetFunc(WeakActionTestCase testCase)
        {
            WeakFunc<T, string> action = null;

            switch (testCase)
            {
                case WeakActionTestCase.PublicNamedMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffPublicallyWithResult);
                    break;
                case WeakActionTestCase.InternalNamedMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffInternallyWithResult);
                    break;
                case WeakActionTestCase.PrivateNamedMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffPrivatelyWithResult);
                    break;
                case WeakActionTestCase.PublicStaticMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffPublicallyAndStaticallyWithResult);
                    break;
                case WeakActionTestCase.PrivateStaticMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffPrivatelyAndStaticallyWithResult);
                    break;
                case WeakActionTestCase.InternalStaticMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        DoStuffInternallyAndStaticallyWithResult);
                    break;
                case WeakActionTestCase.AnonymousStaticMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        p =>
                            {
                                Result = Expected + p;
                                return Result;
                            });
                    break;
                case WeakActionTestCase.AnonymousMethod:
                    action = new WeakFunc<T, string>(
                        this, 
                        p =>
                            {
                                Result = Expected + _index + p;
                                return Result;
                            });
                    break;
            }

            return action;
        }

        internal void DoStuffInternally(T parameter)
        {
            Result = Expected + Internal + _index + parameter;
        }

        internal string DoStuffInternallyWithResult(T parameter)
        {
            Result = Expected + Internal + _index + parameter;
            return Result;
        }

        private static void DoStuffInternallyAndStatically(T parameter)
        {
            Result = Expected + InternalStatic + parameter;
        }

        private static string DoStuffInternallyAndStaticallyWithResult(T parameter)
        {
            Result = Expected + InternalStatic + parameter;
            return Result;
        }

        private static void DoStuffPrivatelyAndStatically(T parameter)
        {
            Result = Expected + PrivateStatic + parameter;
        }

        private static string DoStuffPrivatelyAndStaticallyWithResult(T parameter)
        {
            Result = Expected + PrivateStatic + parameter;
            return Result;
        }

        private void DoStuffPrivately(T parameter)
        {
            Result = Expected + Private + _index + parameter;
        }

        private string DoStuffPrivatelyWithResult(T parameter)
        {
            Result = Expected + Private + _index + parameter;
            return Result;
        }
    }
}
