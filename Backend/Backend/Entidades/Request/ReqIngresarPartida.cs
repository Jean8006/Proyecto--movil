﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entidades.Request
{
    public class ReqIngresarPartida: ReqBase
    {
        public Partida laPartida {  get; set; }
    }
}