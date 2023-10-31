using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Attack : IState
{
    private EnemyAI _enemy;
    public EnemyAI enemy { get { return enemy; }
        set { _enemy = value; } }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Process()
    {

    }
}
