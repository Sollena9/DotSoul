public abstract class EnemyState
{

    //시작할 때 1회 호출 
    public abstract void Enter(Entity_Enemy entity);


    //매 프레임 호출 
    public abstract void Execute(Entity_Enemy entity);


    //나갈 때 1회 호출 
    public abstract void Exit(Entity_Enemy entity);




}
