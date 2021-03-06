using BurningKnight.entity.component;
using BurningKnight.save;
using BurningKnight.ui.editor;
using ImGuiNET;

namespace BurningKnight.entity.room.controllable {
	public class RoomControllable : SaveableEntity, PlaceableEntity {
		public override void AddComponents() {
			base.AddComponents();
			
			AddComponent(new RoomComponent());
			AddComponent(new SupportableComponent());
		}

		public bool On { get; protected set; } = true;
		
		public virtual void TurnOn() {
			On = true;
		}

		public virtual void TurnOff() {
			On = false;
		}

		public void SetState(bool on) {
			if (on != On) {
				Toggle();
			}
		}
		
		public void Toggle() {
			if (On) {
				TurnOff();
			} else {
				TurnOn();
			}
		}

		private bool added;

		public override void Update(float dt) {
			base.Update(dt);

			if (!added) {
				var room = GetComponent<RoomComponent>().Room;

				if (room == null) {
					return;
				}
				
				added = true;
				room.Controllable.Add(this);
			}
		}

		protected void RemoveFromRoom() {
			GetComponent<RoomComponent>().Room?.Controllable.Remove(this);
		}

		public override void RenderImDebug() {
			base.RenderImDebug();

			var on = On;

			if (ImGui.Checkbox("On", ref on)) {
				if (on) {
					TurnOn();
				} else {
					TurnOff();
				}
			}
		}
	}
}