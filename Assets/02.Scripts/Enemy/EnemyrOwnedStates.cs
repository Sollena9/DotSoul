using UnityEngine;

namespace EnemyOwnedStates
{

    public class Idle : EnemyState
    {
        public override void Enter(Entity_Enemy entity) // 시작할때 1번
        {
            entity.anim.SetFloat("idle", 1);
            entity.anim.Play("idle");
            entity.isCombat = false;


        }

        public override void Execute(Entity_Enemy entity) // 매 프레임
        {
            entity.LookChange();
            entity.movement.InputTest();

        }

        public override void Exit(Entity_Enemy entity) // 나갈때 1번 
        {

        }
    }


    public class Move : EnemyState
    {
        public override void Enter(Entity_Enemy entity) // 시작할때 1번
        {
            entity.anim.SetFloat("idle", 0);
            entity.isCombat = false;


            entity.LookChange();
        }

        public override void Execute(Entity_Enemy entity) // 매 프레임
        {
            entity.movement.InputTest();
            entity.movement.Walk();

        }

        public override void Exit(Entity_Enemy entity) // 나갈때 1번 
        {
            entity.anim.SetFloat("Move", 0);

        }
    }


    public class Die : EnemyState
    {
        public override void Enter(Entity_Enemy entity) // 시작할때 1번
        {
            entity.anim.SetBool("Die", true);


        }

        public override void Execute(Entity_Enemy entity) // 매 프레임
        {


        }

        public override void Exit(Entity_Enemy entity) // 나갈때 1번 
        {
            entity.GetOffEngage();

        }
    }


}
