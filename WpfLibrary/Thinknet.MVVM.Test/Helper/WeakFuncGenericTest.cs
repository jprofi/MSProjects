namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakFuncGenericTest
    {
        private WeakFunc<string, string> _action;
        private CommonTestClass _common;
        private InternalTestClass<string> _itemInternal;
        private PublicTestClass<string> _itemPublic;
        private string _local;
        private WeakReference _reference;

        public static void DoStuffStatic(string p)
        {
        }

        public void DoStuff(string p)
        {
            _local = p;
        }

        [Test]
        public void TestInternalClassAnonymousMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + index + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + index + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassAnonymousStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemInternal = new InternalTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Internal + index + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Internal + index + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.InternalStatic + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.InternalStatic + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemInternal = new InternalTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Private + index + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Private + index + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.PrivateStatic + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.PrivateStatic + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>(index);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Public + index + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.Public + index + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPublicStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.PublicStatic + parameter, 
                InternalTestClass<string>.Result);
            Assert.AreEqual(
                InternalTestClass<string>.Expected + InternalTestClass<string>.PublicStatic + parameter, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestNonStaticMethodWithNullTarget()
        {
            Reset();
            WeakAction<string> action = new WeakAction<string>(null, DoStuff);
            Assert.IsFalse(action.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + index + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + index + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPublic = new PublicTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Internal + index + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Internal + index + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.InternalStatic + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.InternalStatic + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPublic = new PublicTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Private + index + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Private + index + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.PrivateStatic + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.PrivateStatic + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>(index);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Public + index + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.Public + index + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPublicStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.PublicStatic + parameter, 
                PublicTestClass<string>.Result);
            Assert.AreEqual(
                PublicTestClass<string>.Expected + PublicTestClass<string>.PublicStatic + parameter, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestStaticMethodWithNonNullTarget()
        {
            Reset();

            _common = new CommonTestClass();
            _reference = new WeakReference(_common);
            Assert.IsTrue(_reference.IsAlive);

            WeakAction<string> action = new WeakAction<string>(_common, DoStuffStatic);
            Assert.IsTrue(action.IsAlive);

            _common = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
            Assert.IsFalse(action.IsAlive);
        }

        [Test]
        public void TestStaticMethodWithNullTarget()
        {
            Reset();
            WeakAction<string> action = new WeakAction<string>(null, DoStuffStatic);
            Assert.IsTrue(action.IsAlive);
        }

        private void Reset()
        {
            _itemPublic = null;
            _itemInternal = null;
            _reference = null;
        }
    }
}
