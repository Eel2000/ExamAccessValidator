using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExamAccessValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamAccessValidator.Droid.Natives
{
    public class ToastMessageDroid : IToastMessage
    {
        public void ShowLong(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShowShort(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}