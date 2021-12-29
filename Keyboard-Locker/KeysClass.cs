namespace Keyboard_Locker;

public static class KeysClass
{
    public static (List<Keys>, List<Keys>) ListProfile;
    public static List<Keys> UnlockList = new();
    public static List<Keys> CustomUnlockList = new();

    private static readonly string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "List.xml");

    public static void LoadXml()
    {
        try
        {
            ListProfile = LoadXml<(List<Keys>, List<Keys>)>(AppPath);


            UnlockList = ListProfile.Item1 is { Count: > 0 } ? ListProfile.Item1 : new List<Keys>();
            CustomUnlockList = ListProfile.Item2 is { Count: > 0 } ? ListProfile.Item2 : new List<Keys>();
        }
        catch
        {
            //
        }
    }

    private static T LoadXml<T>(string fileName)
    {
        var objectOut = default(T);
        try
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            var xmlString = xmlDocument.OuterXml;

            using var read = new StringReader(xmlString);
            var outType = typeof(T);

            var serializer = new XmlSerializer(outType);
            using XmlReader reader = new XmlTextReader(read);
            objectOut = (T)serializer.Deserialize(reader)!;
        }
        catch
        {
            //
        }
        return objectOut ?? default!;
    }

    public static bool SaveXml<T>(T serializableObject, string fileName)
    {
        if (serializableObject == null) { return false; }

        try
        {
            var xmlDocument = new XmlDocument();
            var serializer = new XmlSerializer(serializableObject.GetType());
            using var stream = new MemoryStream();
            serializer.Serialize(stream, serializableObject);
            stream.Position = 0;
            xmlDocument.Load(stream);
            xmlDocument.Save(fileName);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsDuplicate(this Keys key, List<Keys> unlockList)
    {
        return unlockList.Count != 0 && unlockList.Any(t => t == key);
    }

    public static string Key2String(Keys key)
    {
        return key switch
        {
            Keys.Escape => "Escape",
            Keys.F1 => "F1",
            Keys.F2 => "F2",
            Keys.F3 => "F3",
            Keys.F4 => "F4",
            Keys.F5 => "F5",
            Keys.F6 => "F6",
            Keys.F7 => "F7",
            Keys.F8 => "F8",
            Keys.F9 => "F9",
            Keys.F10 => "F10",
            Keys.F11 => "F11",
            Keys.F12 => "F12",

            Keys.Oemtilde => "Oemtilde",
            Keys.D1 => "D1",
            Keys.D2 => "D2",
            Keys.D3 => "D3",
            Keys.D4 => "D4",
            Keys.D5 => "D5",
            Keys.D6 => "D6",
            Keys.D7 => "D7",
            Keys.D8 => "D8",
            Keys.D9 => "D9",
            Keys.D0 => "D0",
            Keys.OemMinus => "OemMinus",
            Keys.Oemplus => "Oemplus",
            Keys.Back => "Back",

            Keys.Tab => "Tab",
            Keys.Q => "Q",
            Keys.W => "W",
            Keys.E => "E",
            Keys.R => "R",
            Keys.T => "T",
            Keys.Y => "Y",
            Keys.U => "U",
            Keys.I => "I",
            Keys.O => "O",
            Keys.P => "P",
            Keys.OemOpenBrackets => "OemOpenBrackets",
            Keys.Oem6 => "Oem6",
            Keys.Oem5 => "Oem5",

            Keys.CapsLock => "CapsLock",
            Keys.A => "A",
            Keys.S => "S",
            Keys.D => "D",
            Keys.F => "F",
            Keys.G => "G",
            Keys.H => "H",
            Keys.J => "J",
            Keys.K => "K",
            Keys.L => "L",
            Keys.Oem1 => "Oem1",
            Keys.Oem7 => "Oem7",
            Keys.Enter => "Enter",

            Keys.LShiftKey => "LShiftKey",
            Keys.Z => "Z",
            Keys.X => "X",
            Keys.C => "C",
            Keys.V => "V",
            Keys.B => "B",
            Keys.N => "N",
            Keys.M => "M",
            Keys.Oemcomma => "Oemcomma",
            Keys.OemPeriod => "OemPeriod",
            Keys.OemQuestion => "OemQuestion",
            Keys.RShiftKey => "RShiftKey",

            Keys.LControlKey => "LControlKey",
            Keys.LWin => "LWin",
            Keys.LMenu => "LMenu",
            Keys.Space => "Space",
            Keys.RMenu => "RMenu",
            Keys.RWin => "RWin",
            Keys.Apps => "Apps",
            Keys.RControlKey => "RControlKey",

            Keys.PrintScreen => "PrintScreen",
            Keys.Scroll => "Scroll",
            Keys.Pause => "Pause",
            Keys.Insert => "Insert",
            Keys.Home => "Home",
            Keys.PageUp => "PageUp",
            Keys.Delete => "Delete",
            Keys.End => "End",
            Keys.PageDown => "PageDown",

            Keys.Up => "Up",
            Keys.Left => "Left",
            Keys.Down => "Down",
            Keys.Right => "Right",

            Keys.NumLock => "NumLock",
            Keys.Divide => "Divide",
            Keys.Multiply => "Multiply",
            Keys.Subtract => "Subtract",
            Keys.Add => "Add",
            //Keys.Return => "Return",
            Keys.Decimal => "Decimal",
            Keys.NumPad0 => "NumPad0",
            Keys.NumPad1 => "NumPad1",
            Keys.NumPad2 => "NumPad2",
            Keys.NumPad3 => "NumPad3",
            Keys.NumPad4 => "NumPad4",
            Keys.NumPad5 => "NumPad5",
            Keys.NumPad6 => "NumPad6",
            Keys.NumPad7 => "NumPad7",
            Keys.NumPad8 => "NumPad8",
            Keys.NumPad9 => "NumPad9",

            Keys.VolumeMute => "VolumeMute",
            Keys.VolumeDown => "VolumeDown",
            Keys.VolumeUp => "VolumeUp",

            Keys.MediaPlayPause => "MediaPlayPause",
            Keys.MediaStop => "MediaStop",
            Keys.MediaPreviousTrack => "MediaPreviousTrack",
            Keys.MediaNextTrack => "MediaNextTrack",


            _ => ""
        };
    }

    public static Keys String2Key(string key)
    {
        return key switch
        {
            "Escape" => Keys.Escape,
            "F1" => Keys.F1,
            "F2" => Keys.F2,
            "F3" => Keys.F3,
            "F4" => Keys.F4,
            "F5" => Keys.F5,
            "F6" => Keys.F6,
            "F7" => Keys.F7,
            "F8" => Keys.F8,
            "F9" => Keys.F9,
            "F10" => Keys.F10,
            "F11" => Keys.F11,
            "F12" => Keys.F12,

            "Oemtilde" => Keys.Oemtilde,
            "D1" => Keys.D1,
            "D2" => Keys.D2,
            "D3" => Keys.D3,
            "D4" => Keys.D4,
            "D5" => Keys.D5,
            "D6" => Keys.D6,
            "D7" => Keys.D7,
            "D8" => Keys.D8,
            "D9" => Keys.D9,
            "D0" => Keys.D0,
            "OemMinus" => Keys.OemMinus,
            "Oemplus" => Keys.Oemplus,
            "Back" => Keys.Back,

            "Tab" => Keys.Tab,
            "Q" => Keys.Q,
            "W" => Keys.W,
            "E" => Keys.E,
            "R" => Keys.R,
            "T" => Keys.T,
            "Y" => Keys.Y,
            "U" => Keys.U,
            "I" => Keys.I,
            "O" => Keys.O,
            "P" => Keys.P,
            "OemOpenBrackets" => Keys.OemOpenBrackets,
            "Oem6" => Keys.Oem6,
            "Oem5" => Keys.Oem5,

            "CapsLock" => Keys.CapsLock,
            "A" => Keys.A,
            "S" => Keys.S,
            "D" => Keys.D,
            "F" => Keys.F,
            "G" => Keys.G,
            "H" => Keys.H,
            "J" => Keys.J,
            "K" => Keys.K,
            "L" => Keys.L,
            "Oem1" => Keys.Oem1,
            "Oem7" => Keys.Oem7,
            "Enter" => Keys.Enter,

            "LShiftKey" => Keys.LShiftKey,
            "Z" => Keys.Z,
            "X" => Keys.X,
            "C" => Keys.C,
            "V" => Keys.V,
            "B" => Keys.B,
            "N" => Keys.N,
            "M" => Keys.M,
            "Oemcomma" => Keys.Oemcomma,
            "OemPeriod" => Keys.OemPeriod,
            "OemQuestion" => Keys.OemQuestion,
            "RShiftKey" => Keys.RShiftKey,

            "LControlKey" => Keys.LControlKey,
            "LWin" => Keys.LWin,
            "LMenu" => Keys.LMenu,
            "Space" => Keys.Space,
            "RMenu" => Keys.RMenu,
            "RWin" => Keys.RWin,
            "Apps" => Keys.Apps,
            "RControlKey" => Keys.RControlKey,

            "PrintScreen" => Keys.PrintScreen,
            "Scroll" => Keys.Scroll,
            "Pause" => Keys.Pause,
            "Insert" => Keys.Insert,
            "Home" => Keys.Home,
            "PageUp" => Keys.PageUp,
            "Delete" => Keys.Delete,
            "End" => Keys.End,
            "PageDown" => Keys.PageDown,

            "Up" => Keys.Up,
            "Left" => Keys.Left,
            "Down" => Keys.Down,
            "Right" => Keys.Right,

            "NumLock" => Keys.NumLock,
            "Divide" => Keys.Divide,
            "Multiply" => Keys.Multiply,
            "Subtract" => Keys.Subtract,
            "Add" => Keys.Add,
            "Return" => Keys.Return,
            "Decimal" => Keys.Decimal,
            "NumPad0" => Keys.NumPad0,
            "NumPad1" => Keys.NumPad1,
            "NumPad2" => Keys.NumPad2,
            "NumPad3" => Keys.NumPad3,
            "NumPad4" => Keys.NumPad4,
            "NumPad5" => Keys.NumPad5,
            "NumPad6" => Keys.NumPad6,
            "NumPad7" => Keys.NumPad7,
            "NumPad8" => Keys.NumPad8,
            "NumPad9" => Keys.NumPad9,

            "VolumeMute" => Keys.VolumeMute,
            "VolumeDown" => Keys.VolumeDown,
            "VolumeUp" => Keys.VolumeUp,

            "F13" => Keys.F13,
            "F14" => Keys.F14,
            "F15" => Keys.F15,
            "F16" => Keys.F16,
            "F17" => Keys.F17,
            "F18" => Keys.F18,
            "F19" => Keys.F19,
            "F20" => Keys.F20,
            "F21" => Keys.F21,
            "F22" => Keys.F22,
            "F23" => Keys.F23,
            "F24" => Keys.F24,

            "MediaPlayPause" => Keys.MediaPlayPause,
            "MediaStop" => Keys.MediaStop,
            "MediaPreviousTrack" => Keys.MediaPreviousTrack,
            "MediaNextTrack" => Keys.MediaNextTrack,

            _ => Keys.None
        };
    }
}