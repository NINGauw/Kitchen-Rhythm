using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar
{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
    public class OnProgressChangeEventArgs : EventArgs{
        public float progressNomalized;
    }
}
