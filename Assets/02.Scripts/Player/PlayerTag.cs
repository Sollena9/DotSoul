using System;

[Flags]
public enum State
{
    _Idle = 0,
    _Move = 1 << 0,
    _Jump = 1 << 1,
    _Attack = 1 << 2,
    _Skill = 1 << 3,
    _Guard = 1 << 4,
    _Estus = 1 << 5,
    _Dodge = 1 << 6,
    _Parry = 1 << 7,
    _Grab = 1 << 8,
    _Die = 1 << 9,
    _Combat = 1 << 10



}
public class PlayerTag : TagSystem<State> { }


