using System;

public interface MVCIData
{
}

public interface MVCIView
{
    void Initialize();
    void SpawnModel( Action onComplete);
}

public interface MVCIController
{
    void SetModel(MVCIData data);
    void SetView(MVCIView view);
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Initialize();
}