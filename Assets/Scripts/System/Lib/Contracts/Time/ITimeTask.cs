﻿
using System;

namespace CatLib.Contracts.Time
{

    /// <summary>
    /// 时间任务
    /// </summary>
    public interface ITimeTask
    {

        ITimeTask Delay(float time);

        ITimeTask Loop(float time);

        ITimeTask Loop(Func<float> loopFunc);

        ITimeTask Loop(Func<bool> loopFunc);

        ITimeTask OnComplete(Action onComplete);

        ITimeTask OnComplete(Action<object> onComplete);

        ITimeTask Task(Action task);

        ITimeTask Task(Action<object> task);

        ITimeTaskHandler Push();

    }

}