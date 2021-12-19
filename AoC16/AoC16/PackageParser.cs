using System;
using System.Collections.Generic;

namespace AoC16
{
    public static class PackageParser
    {
        public static Package Parse(string data)
        {
            return parseSinglePackage(ref data);
        }

        private static Package parseSinglePackage(ref string data)
        {
            var version = Convert.ToInt16(data.Substring(0, 3), 2);
            var type = (EnmPackageType)Convert.ToInt16(data.Substring(3, 3), 2);
            data = data.Substring(6);

            if (type == EnmPackageType.Literal)
            {
                return new LiteralPackage
                {
                    Version = version,
                    Type = type,
                    Value = parseLiteralData(ref data)
                };
            }

            var lengthType = (EnmLengthType)int.Parse(data.Substring(0, 1));
            data = data.Substring(1);

            if (lengthType == EnmLengthType.NumberOfPackages)
            {
                int numberOfPackages = Convert.ToInt32(data.Substring(0, 11), 2);
                data = data.Substring(11);
                return new OperatorPackageNumber
                {
                    Version = version,
                    Type = type,
                    LengthType = lengthType,
                    NumberOfPackages = numberOfPackages,
                    Packages = parseNumberOfPackages(ref data, numberOfPackages),
                };
            }

            var dataLength = Convert.ToInt32(data.Substring(0, 15), 2);
            data = data.Substring(15);

            var payload = data.Substring(0, dataLength);
            data = data.Substring(dataLength);

            return new OperatorPackageLength
            {
                Version = version,
                Type = type,
                LengthType = lengthType,
                DataLength = dataLength,
                Packages = parseAllPackages(payload),
            };
        }

        private static List<Package> parseAllPackages(string data)
        {
            var packages = new List<Package>();
            while (data.Length > 6)
            {
                packages.Add(parseSinglePackage(ref data));
            }

            return packages;
        }

        private static List<Package> parseNumberOfPackages(ref string data, int packageCount)
        {
            var packages = new List<Package>();
            for (; packageCount > 0; packageCount--)
            {
                packages.Add(parseSinglePackage(ref data));
            }

            return packages;
        }

        private static long parseLiteralData(ref string data)
        {
            bool keepReading;
            var value = string.Empty;
            do
            {
                keepReading = '1'.Equals(data[0]);
                value += data.Substring(1, 4);
                data = data.Substring(5);
            } while (keepReading);

            return Convert.ToInt64(value, 2);
        }
    }
}