﻿// Copyright (C) RelaperCrystal 2019, 2020
// This file is part of HotCallouts for Grand Theft Auto V.

// HotCallouts for Grand Theft Auto V (or HotCalloutsV)
// is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// HotCalloutsV is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with HotCalloutsV.  If not, see <https://www.gnu.org/licenses/>. 

using HotCalloutsV.Entities.Interfaces;
using Rage;

namespace HotCalloutsV.Entities
{
    public class TextEntire : ChatEntire
    {
        public string Context { get; private set; }

        public override bool IsFunctional { get => false; }

        public override void Function()
        {
            Game.DisplaySubtitle(Context);
        }

        public override void Function(Ped p)
        {
            Function();
        }

        public TextEntire(string text)
        {
            Context = text;
        }

        public static implicit operator TextEntire(string text)
        {
            return new TextEntire(text);
        }
    }
}