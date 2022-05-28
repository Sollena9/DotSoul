using UnityEngine;

namespace PlayerOwnedStates
{

    public class Idle : PlayerState
    {
        public override void Enter(Entity entity) // 시작할때 1번
        {
            entity.anim.Play("idle");

            if (entity.isGrounded)
                entity.jumpCounter = 1;

        }

        public override void Execute(Entity entity) // 매 프레임
        {
            entity.LookChange();


        }

        public override void Exit(Entity entity) // 나갈때 1번 
        {

        }
    }


    public class Move : PlayerState
    {
        public override void Enter(Entity entity) // 시작할때 1번
        {
            entity.anim.Play("idle");

            if (entity.isGrounded)
                entity.jumpCounter = 1;

            entity.LookChange();
        }

        public override void Execute(Entity entity) // 매 프레임
        {


        }

        public override void Exit(Entity entity) // 나갈때 1번 
        {

        }
    }

    public class Jump : PlayerState
    {
        public override void Enter(Entity entity) // 시작할때 1번
        {
            entity.anim.Play("Jump");

            if (entity.isGrounded)
                entity.jumpCounter = 0;

        }

        public override void Execute(Entity entity) // 매 프레임
        {
            entity.LookChange();

        }

        public override void Exit(Entity entity) // 나갈때 1번 
        {

        }
    }

    public class Die : PlayerState
    {
        public override void Enter(Entity entity) // 시작할때 1번
        {
            entity.anim.SetBool("Die", true);

            GameManager.instance.GetComponent<SystemManager>().StartCoroutine("PrintYouDied");


        }

        public override void Execute(Entity entity) // 매 프레임
        {


        }

        public override void Exit(Entity entity) // 나갈때 1번 
        {

        }
    }


}
