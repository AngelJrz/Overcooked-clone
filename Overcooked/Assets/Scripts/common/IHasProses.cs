using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProses
{
    public event EventHandler<OnProgressActionEventArgs> OnProgressAction;
    public class OnProgressActionEventArgs : EventArgs {
        public float progress;
    }
}
