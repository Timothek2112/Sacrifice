using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IState
{
    public EnemyAI enemy { get; set; }
    public void Enter();
    public void Process();
    public void Exit();
}

