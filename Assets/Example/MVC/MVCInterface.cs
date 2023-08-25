using System;

public interface MVCIData
{
}

public interface MVCView
{
    public void SpawnModel( Action onComplete);
}

public interface MVCController
{
    public void SetModel(MVCIData data);
    public void SetView(MVCView view);
    void Update();
    void LateUpdate();
    void Initialize();
}