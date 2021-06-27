
namespace Mindbox
{
    /// <summary>
    /// Интерфейс, представляющий геометрическую фигуру.
    /// </summary>
    public interface IShape
    {
        float Area { get; }

        float Perimeter { get; }
    }
}
