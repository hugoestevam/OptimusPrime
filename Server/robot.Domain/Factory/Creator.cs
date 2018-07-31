using System;

namespace robot.Domain.Factory
{
    /// <summary>
    /// Classe abstrata que define 
    /// como será invocada a criação do Robo
    /// </summary>
    public abstract class Creator
    {
        public abstract Robot MakeARobot();
    }
}
