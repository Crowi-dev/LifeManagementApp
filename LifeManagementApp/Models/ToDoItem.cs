using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManagementApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Priority { get; set; }      // 1 = high, 3 = low
    public DateTime DueDate { get; set; }

    [NotMapped]
    public string Info => $"Priority {Priority} · due {DueDate:d.M.yyyy}";
}