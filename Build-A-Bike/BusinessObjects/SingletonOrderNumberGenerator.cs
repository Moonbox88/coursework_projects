using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public sealed class SingletonOrderNumberGenerator
    {
        private static SingletonOrderNumberGenerator instance;

        private int _order_num = 0;
        private int _bike_num = 0;

        private SingletonOrderNumberGenerator() { }

        public static SingletonOrderNumberGenerator Instance()
        {
            if (instance == null)
            {
                instance = new SingletonOrderNumberGenerator();
            }
            return instance;
        }

        public int generateOrderReference()
        {
            _order_num++;

            return _order_num;
        }

        public int generateBikeReference()
        {
            _bike_num++;

            return _bike_num;
        }
    }
}
