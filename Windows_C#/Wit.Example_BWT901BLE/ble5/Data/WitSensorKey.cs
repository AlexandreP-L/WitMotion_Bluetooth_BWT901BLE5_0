using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wit.SDK.Device.Device.Device.DKey;

namespace Wit.SDK.Modular.Sensor.Modular.DataProcessor.Constant
{
    /// <summary>
    /// 倾角传感器标准key常量
    /// </summary>
    public static class WitSensorKey
    {
        // 芯片时间
        public static StringKey ChipTime { get; } = new StringKey("ChipTime");

        // AccelerationX
        public static DoubleKey AccX { get; } = new DoubleKey("AccX");

        // Acceleration Y
        public static DoubleKey AccY { get; } = new DoubleKey("AccY");

        // Acceleration Z
        public static DoubleKey AccZ { get; } = new DoubleKey("AccZ");

        // Acceleration vector sum
        public static DoubleKey AccM { get; } = new DoubleKey("AccM");

        // angular velocity X
        public static DoubleKey AsX { get; } = new DoubleKey("AsX");

        // angular velocity Y
        public static DoubleKey AsY { get; } = new DoubleKey("AsY");

        // angular velocity Z
        public static DoubleKey AsZ { get; } = new DoubleKey("AsZ");

        // Angular velocity vector sum
        public static DoubleKey AsM { get; } = new DoubleKey("AsM");

        // AngleX
        public static DoubleKey AngleX { get; } = new DoubleKey("AngleX");

        // Angle Y
        public static DoubleKey AngleY { get; } = new DoubleKey("AngleY");

        // Angle Z
        public static DoubleKey AngleZ { get; } = new DoubleKey("AngleZ");

        // Magnetic field X
        public static DoubleKey HX { get; } = new DoubleKey("HX");

        // Magnetic field Y
        public static DoubleKey HY { get; } = new DoubleKey("HY");

        // Magnetic field Z
        public static DoubleKey HZ { get; } = new DoubleKey("HZ");

        // Magnetic field vector sum
        public static DoubleKey HM { get; } = new DoubleKey("HM");

        // Temperature
        public static DoubleKey T { get; } = new DoubleKey("T");

        // Expansion port 1
        public static ShortKey D0 { get; } = new ShortKey("D0");

        // Expansion port 2
        public static ShortKey D1 { get; } = new ShortKey("D1");

        // Expansion port 3
        public static ShortKey D2 { get; } = new ShortKey("D2");

        // Expansion port 4
        public static ShortKey D3 { get; } = new ShortKey("D3");

        // Air Pressure
        public static DoubleKey P { get; } = new DoubleKey("P");

        // High
        public static DoubleKey H { get; } = new DoubleKey("H");

        // Longitude
        public static DoubleKey Lon { get; } = new DoubleKey("Lon");

        // Longitude degree representation
        public static DoubleKey LonDeg { get; } = new DoubleKey("LonDeg");

        // Latitude
        public static DoubleKey Lat { get; } = new DoubleKey("Lat");

        // Latitude degree representation
        public static DoubleKey LatDeg { get; } = new DoubleKey("LatDeg");

        /// <summary>
        /// GPS status
        /// </summary>
        public static DoubleKey GPSStatus { get; } = new DoubleKey("GPSStatus");


        // GPS altitude
        public static DoubleKey GPSHeight { get; } = new DoubleKey("GPSHeight");

        // GPS航向
        public static DoubleKey GPSYaw { get; } = new DoubleKey("GPSYaw");

        // GPS ground speed
        public static DoubleKey GPSV { get; } = new DoubleKey("GPSV");

        // quaternion 0
        public static DoubleKey Q0 { get; } = new DoubleKey("Q0");

        // quaternion 1
        public static DoubleKey Q1 { get; } = new DoubleKey("Q1");

        // quaternion 2
        public static DoubleKey Q2 { get; } = new DoubleKey("Q2");

        // quaternion 3
        public static DoubleKey Q3 { get; } = new DoubleKey("Q3");

        // Number of satellites
        public static IntKey SN { get; } = new IntKey("SN");

        // Position positioning accuracy
        public static DoubleKey PDOP { get; } = new DoubleKey("PDOP");

        // Horizontal positioning accuracy
        public static DoubleKey HDOP { get; } = new DoubleKey("HDOP");

        // Vertical positioning accuracy
        public static DoubleKey VDOP { get; } = new DoubleKey("VDOP");

        // Version number
        public static StringKey VersionNumber { get; } = new StringKey("VersionNumber");

        // Serial Number
        public static StringKey SerialNumber { get; } = new StringKey("SerialNumber");

        // Battery
        public static DoubleKey PowerPercent { get; } = new DoubleKey("PowerPercent");
    }
}
