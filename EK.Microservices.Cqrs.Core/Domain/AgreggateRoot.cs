using EK.Microservices.Cqrs.Core.Events;
using System.Reflection;

namespace EK.Microservices.Cqrs.Core.Domain
{
    public abstract class AgreggateRoot
    {
        public string Id { get; set; } = string.Empty;
        public int version = -1;
        List<BaseEvent> changes = new List<BaseEvent>();

        public int GetVersion()
        {
            return version;
        }
        public void SetVersion(int version)
        {
            this.version = version;
        }
        public List<BaseEvent> GetUnCommitedChanges()
        {
            return changes;
        }
        public void MarkChangesAsCommited()
        {
            changes.Clear();
        }
        public void ApplyChanges(BaseEvent @event, bool isNewEvent)
        {
            try
            {
                var ClaseDeEvento = @event.GetType();
                var method = GetType().GetMethod("Apply", new[] { ClaseDeEvento });
                method.Invoke(this, new object[] { @event });
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (isNewEvent)
                    changes.Add(@event);
            }
        }
        public void RaiseEvent(BaseEvent @event)
        {
            ApplyChanges(@event, true);
        }
        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach (var evt in events)
            {
                ApplyChanges(evt, false);
            }
        }
    }
}
