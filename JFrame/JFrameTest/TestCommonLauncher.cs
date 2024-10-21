//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestCommonLauncher
    {
        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void Clear()
        {

        }

        [Test]
        public void Test_run_status_changed_after_running()
        {
            //arrange
            var r = Substitute.ForPartsOf<BaseRunable>(); //运行真实代码

            //act
            r.Run(null);

            //expect
            Assert.AreEqual(true, r.IsRunning);
        }

        [Test]
        public void Test_stop_a_running_object_and_running_is_false()
        {
            //arrange
            var r = Substitute.ForPartsOf<BaseRunable>(); //运行真实代码

            //act
            r.Run(null);
            Assert.AreEqual(true, r.IsRunning);
            r.Stop();

            //expect
            Assert.AreEqual(false, r.IsRunning);

        }

        [Test]
        public void TestRunR1()
        {
            //arrange
            var r1 = Substitute.For<IRunable>();
            var r2 = Substitute.For<IRunable>();
            var que = new Queue<IRunable>() { };
            que.Enqueue(r1); 
            que.Enqueue(r2);
            RunableExtraData extraData = null;
            var gameDirector = new CommonLauncher(que);

            //act
            gameDirector.Run(extraData);

            //expect
            r1.Received(1).Run(extraData);
        }

        /// <summary>
        /// 依次运行R1和R2
        /// </summary>
        [Test]
        public void TestRunR1AndR2()
        {
            //arrange
            var r1 = Substitute.For<IRunable>();
            var r2 = Substitute.For<IRunable>();
            var que = new Queue<IRunable>() { };
            que.Enqueue(r1);
            que.Enqueue(r2);
            RunableExtraData extraData = null;
            var gameDirector = new CommonLauncher(que);

            //act
            gameDirector.Run(extraData);
            r1.onComplete += Raise.Event<Action<IRunable>>(r1);

            //expect
            r1.Received(1).Run(extraData);
            r2.Received(1).Run(extraData);
        }

        [Test]
        public void TestRunAndThrowException()
        {
            //arrange
            var gameDirector = new CommonLauncher(null);
            RunableExtraData extraData = null;

            //act
            Assert.Throws<NullReferenceException>( ()=> gameDirector.Run(extraData)) ;
        }
    }
}
