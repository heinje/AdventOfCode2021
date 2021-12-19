using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC16
{
    public abstract class OperatorPackage : Package
    {
        public EnmLengthType LengthType { get; set; }
        public List<Package> Packages { get; set; }

        public override int GetVersionSum()
        {
            return Version + Packages.Sum(package => package.GetVersionSum());
        }

        public override long GetValue()
        {
            switch (Type)
            {
                case EnmPackageType.Sum:
                    return Packages.Sum(package => package.GetValue());
                case EnmPackageType.Product:
                    return calculateProduct(Packages);
                case EnmPackageType.Minimum:
                    return Packages.Min(package => package.GetValue());
                case EnmPackageType.Maximum:
                    return Packages.Max(package => package.GetValue());
                case EnmPackageType.GreaterThan:
                    return Packages[0].GetValue() > Packages[1].GetValue() ? 1 : 0;
                case EnmPackageType.LessThan:
                    return Packages[0].GetValue() < Packages[1].GetValue() ? 1 : 0;
                case EnmPackageType.EqualTo:
                    return Packages[0].GetValue() == Packages[1].GetValue() ? 1 : 0;
                default:
                    throw new Exception("ERROR: Unkown Operation Type!");
            }
        }

        private long calculateProduct(List<Package> packages)
        {
            long result = 1;
            foreach(var package in packages)
            {
                result *= package.GetValue();
            }
            return result;
        }
    }
}