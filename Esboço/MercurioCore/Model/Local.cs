using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class Local
    {
        public int Id { get; private set; }
        public virtual CheckPoint checkPoint { get; private set; }
        public virtual Rua rua { get; private set; }
        public Local(int id, CheckPoint checkPoint, Rua rua)
        {
            Id = id;
            this.checkPoint = checkPoint;
            this.rua = rua;
        }
    }
}
