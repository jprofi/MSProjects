namespace Thinknet.MVVM.Test.Helper
{
    using System;
    using System.Globalization;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakFuncTest
    {
        private WeakFunc<string> _action;
        private CommonTestClass _common;
        private InternalTestClass _itemInternal;
        private PublicTestClass _itemPublic;
        private string _local;
        private WeakReference _reference;

        public static string DoStuffStaticWithResult()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        public string DoStuffWithResult()
        {
            _local = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            return _local;
        }

        [Test]
        public void TestInternalClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + index, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + index, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassAnonymousStaticMethod()
        {
            Reset();

            _itemInternal = new InternalTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Internal + index, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Internal + index, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalStaticMethod()
        {
            Reset();

            _itemInternal = new InternalTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.InternalStatic, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.InternalStatic, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Private + index, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Private + index, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateStaticMethod()
        {
            Reset();

            _itemInternal = new InternalTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PrivateStatic, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PrivateStatic, 
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

            _itemInternal = new InternalTestClass(index);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Public + index, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Public + index, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPublicStaticMethod()
        {
            Reset();

            _itemInternal = new InternalTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PublicStatic, 
                InternalTestClass.Result);
            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PublicStatic, 
                result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestNonStaticMethodWithNullTarget()
        {
            Reset();
            WeakFunc<string> func = new WeakFunc<string>(null, DoStuffWithResult);
            Assert.IsFalse(func.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + index, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + index, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousStaticMethod()
        {
            Reset();

            _itemPublic = new PublicTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Internal + index, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Internal + index, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalStaticMethod()
        {
            Reset();

            _itemPublic = new PublicTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.InternalStatic, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.InternalStatic, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Private + index, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Private + index, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateStaticMethod()
        {
            Reset();

            _itemPublic = new PublicTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PrivateStatic, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PrivateStatic, 
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

            _itemPublic = new PublicTestClass(index);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Public + index, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Public + index, 
                result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPublicStaticMethod()
        {
            Reset();

            _itemPublic = new PublicTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PublicStatic, 
                PublicTestClass.Result);
            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PublicStatic, 
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

            WeakFunc<string> func = new WeakFunc<string>(_common, DoStuffStaticWithResult);
            Assert.IsTrue(func.IsAlive);

            _common = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
            Assert.IsFalse(func.IsAlive);
        }

        [Test]
        public void TestStaticMethodWithNullTarget()
        {
            Reset();
            WeakFunc<string> func = new WeakFunc<string>(null, DoStuffStaticWithResult);
            Assert.IsTrue(func.IsAlive);
        }

        private void Reset()
        {
            _itemPublic = null;
            _itemInternal = null;
            _reference = null;
        }
    }
}
