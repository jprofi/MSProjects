namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakActionTest
    {
        private WeakAction _action;
        private CommonTestClass _common;
        private InternalTestClass _itemInternal;
        private PublicTestClass _itemPublic;
        private string _local;
        private WeakReference _reference;

        public static void DoStuffStatic()
        {
        }

        public void DoStuff()
        {
            _local = DateTime.Now.ToString();
        }

        [Test]
        public void TestInternalClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + index, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Internal + index, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.InternalStatic, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Private + index, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PrivateStatic, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.Public + index, 
                InternalTestClass.Result);

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

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalTestClass.Expected + InternalTestClass.PublicStatic, 
                InternalTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestNonStaticMethodWithNullTarget()
        {
            Reset();
            WeakAction action = new WeakAction(null, DoStuff);
            Assert.IsFalse(action.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + index, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Internal + index, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.InternalStatic, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Private + index, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PrivateStatic, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.Public + index, 
                PublicTestClass.Result);

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

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicTestClass.Expected + PublicTestClass.PublicStatic, 
                PublicTestClass.Result);

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

            WeakAction action = new WeakAction(_common, DoStuffStatic);
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
            WeakAction action = new WeakAction(null, DoStuffStatic);
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
