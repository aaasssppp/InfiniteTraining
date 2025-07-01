using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    class MobilePhone
    {
        public delegate void RingEventHandler();
        public event RingEventHandler OnRing;
        public void ReceiveCall()
        {
            OnRing?.Invoke();
        }
    }
    class Subscriber
    {
        public void RingtonePlayer()
        {
            Console.WriteLine("Playing Ringtone...");
        }
        public void ReceiveCall()
        {
            Console.WriteLine("Displaying caller information...");
        }
        public void VibrationMotor()
        {
            Console.WriteLine("Phone is vibrating...");
        }
    }
    class MobilePhoneRingNotificationSystem
    {
        static void Main()
        {
            MobilePhone mobilephone = new MobilePhone();
            Subscriber subscriber = new Subscriber();

            // event
            mobilephone.OnRing += subscriber.RingtonePlayer;
            mobilephone.OnRing += subscriber.ReceiveCall;
            mobilephone.OnRing += subscriber.VibrationMotor;

            // event call using ReceiveCall method
            mobilephone.ReceiveCall();

            Console.ReadLine();
        }
    }
}
