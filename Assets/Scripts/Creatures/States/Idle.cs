using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures.States
{
    public class Idle : IState
    {
        private EnemyAI _enemy;
        public EnemyAI enemy { get { return _enemy; } set { _enemy = value; } }

        public Idle(EnemyAI enemy)
        {
            this.enemy = enemy;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void Process()
        {
            if (30 >= Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position))
            {
                enemy.EnterState(new WalkForPlayer(enemy));
            }
        }
    }
}
