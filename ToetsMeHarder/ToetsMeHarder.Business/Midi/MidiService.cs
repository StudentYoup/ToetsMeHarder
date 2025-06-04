using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using ToetsMeHarder.Business.FallingBlocks;
using Usb.Events;

namespace ToetsMeHarder.Business.Midi
{
    public class MidiService : IDisposable
    {
        private InputDevice _inputDevice;
        private IUsbEventWatcher _usbWatcher;

        public event Action<int, int, int> OnMidiDown;
        public event Action<int, int, int> OnMidiUp;        
        public event Action<string> OnMidiConnected;
        public event Action<string> OnMidiDisconnected;
        public string MidiName { get; private set; }

        public MidiService()
        {
            _usbWatcher = new UsbEventWatcher(
                startImmediately: false,
                addAlreadyPresentDevicesToList: true,
                usePnPEntity: true
            );

            _usbWatcher.UsbDeviceAdded += Watcher_DeviceAdded;
            _usbWatcher.UsbDeviceRemoved += Watcher_DeviceRemoved;

            TryOpenFirstDevice();
        }

        public void StartUSBWatcher()
        {
            _usbWatcher.Start();
        }
        private void TryOpenFirstDevice()
        {
            var first = InputDevice.GetAll().FirstOrDefault();
            if (first != null)
                OpenDevice(first);
        }

        private void Watcher_DeviceAdded(object? sender, UsbDevice usb)
        {
            if (_inputDevice == null)
                TryOpenFirstDevice();
        }

        private void Watcher_DeviceRemoved(object? sender, UsbDevice usb)
        {
            if (_inputDevice != null
                && InputDevice.GetDevicesCount() == 0)
            {
                CloseCurrentDevice();
            }
        }

        private void OpenDevice(InputDevice device)
        {
            _inputDevice = device;
            MidiName = device.Name;

            _inputDevice.EventReceived += InputDevice_EventReceived;
            _inputDevice.StartEventsListening();

            OnMidiConnected?.Invoke(MidiName);
        }

        private void CloseCurrentDevice()
        {
            if (_inputDevice == null) return;

            _inputDevice.EventReceived -= InputDevice_EventReceived;
            try
            {
                _inputDevice.StopEventsListening();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _inputDevice.Dispose();
            _inputDevice = null;

            OnMidiDisconnected?.Invoke(MidiName);

            MidiName = null;
        }

        private void InputDevice_EventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            switch (e.Event)
            {
                case NoteOnEvent noteOn:
                    if (noteOn.Velocity == 0)
                        OnMidiUp?.Invoke(0x80, noteOn.NoteNumber, 0);
                    else
                        OnMidiDown?.Invoke(0x90, noteOn.NoteNumber, noteOn.Velocity);
                    break;

                case NoteOffEvent noteOff:
                    OnMidiUp?.Invoke(0x80, noteOff.NoteNumber, noteOff.Velocity);
                    break;
            }
        }

        public void StopListening()
        {
            CloseCurrentDevice();
        }

        public void Dispose()
        {
            StopListening();
            _usbWatcher.UsbDeviceAdded -= Watcher_DeviceAdded;
            _usbWatcher.UsbDeviceRemoved -= Watcher_DeviceRemoved;
        }
        public Dictionary<int, KeyValue> midiNotes = new Dictionary<int, KeyValue>
        {
            { 21, KeyValue.a2 },
            { 22, KeyValue.a2 },
            { 23, KeyValue.a2 },
            { 24, KeyValue.a2 },
            { 25, KeyValue.a2 },
            { 26, KeyValue.a2 },
            { 27, KeyValue.a2 },
            { 28, KeyValue.a2 },
            { 29, KeyValue.a2 },
            { 30, KeyValue.a2 },
            { 31, KeyValue.a2 },
            { 32, KeyValue.a2 },
            { 33, KeyValue.a2},
            { 34, KeyValue.a2 },
            { 35, KeyValue.a2},
            { 36, KeyValue.a2},
            { 37, KeyValue.a2 },
            { 38, KeyValue.a2},
            { 39, KeyValue.a2 },
            { 40, KeyValue.a2},
            { 41, KeyValue.a2},
            { 42, KeyValue.a2 },
            { 43, KeyValue.a2},
            { 44, KeyValue.a2 },
            { 45, KeyValue.a2 },
            { 46, KeyValue.a21 },
            { 47, KeyValue.b2 },
            { 48, KeyValue.c3 },
            { 49, KeyValue.c31 },
            { 50, KeyValue.d3},
            { 51, KeyValue.d31 },
            { 52, KeyValue.e3},
            { 53, KeyValue.f3},
            { 54, KeyValue.f31 },
            { 55, KeyValue.g3},
            { 56, KeyValue.g31 },
            { 57, KeyValue.a3},
            { 58, KeyValue.a31 },
            { 59, KeyValue.b3},
            { 60, KeyValue.c4},
            { 61, KeyValue.c41 },
            { 62, KeyValue.d4},
            { 63, KeyValue.d41 },
            { 64, KeyValue.e4},
            { 65, KeyValue.f4},
            { 66, KeyValue.f41 },
            { 67, KeyValue.g4},
            { 68, KeyValue.g41 },
            { 69, KeyValue.a4},
            { 70, KeyValue.a41 },
            { 71, KeyValue.b4},
            { 72, KeyValue.c5},
            { 73, KeyValue.c51 },
            { 74, KeyValue.d5},
            { 75, KeyValue.d51 },
            { 76, KeyValue.e5},
            { 77, KeyValue.f5},
            { 78, KeyValue.f51 },
            { 79, KeyValue.g5},
            { 80, KeyValue.g51 },
            { 81, KeyValue.a5},
            { 82, KeyValue.a51 },
            { 83, KeyValue.b5},
            { 84, KeyValue.c6},
            { 85, KeyValue.c6 },
            { 86, KeyValue.c6},
            { 87, KeyValue.c6 },
            { 88, KeyValue.c6},
            { 89, KeyValue.c6},
            { 90, KeyValue.c6 },
            { 91, KeyValue.c6},
            { 92, KeyValue.c6 },
            { 93, KeyValue.c6},
            { 94, KeyValue.c6 },
            { 95, KeyValue.c6},
            { 96, KeyValue.c6},
            { 97, KeyValue.c6 },
            { 98, KeyValue.c6},
            { 99, KeyValue.c6 },
            { 100, KeyValue.c6},
            { 101, KeyValue.c6},
            { 102, KeyValue.c6 },
            { 103, KeyValue.c6},
            { 104, KeyValue.c6 },
            { 105, KeyValue.c6},
            { 106, KeyValue.c6 },
            { 107, KeyValue.c6},
            { 108, KeyValue.c6}
        };
    }
}
