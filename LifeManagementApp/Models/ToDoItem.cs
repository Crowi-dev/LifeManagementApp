using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManagementApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Priority { get; set; }      // 1–5, 5 = most important
    public DateTime DueDate { get; set; }
    public bool IsDone { get; set; }

    [NotMapped]
    public string Info => $"{(IsDone ? "✅ Done · " : "")}Priority {Priority} · due {DueDate:d.M.yyyy}";
}