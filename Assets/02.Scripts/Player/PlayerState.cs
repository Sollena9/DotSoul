
public abstract class PlayerState 
{
    //시작할 때 1회 호출 
    public abstract void Enter(Entity entity);


    //매 프레임 호출 
    public abstract void Execute(Entity entity);


    //나갈 때 1회 호출 
    public abstract void Exit(Entity entity);

}
