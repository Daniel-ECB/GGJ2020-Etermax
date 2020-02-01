public interface IPooling {
    void Init();
    bool IsUsing { get; }
    void OnGet();
    void OnRelease();
}