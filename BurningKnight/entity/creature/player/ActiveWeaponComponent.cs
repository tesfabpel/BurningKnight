using System;
using BurningKnight.assets.input;
using BurningKnight.entity.buff;
using BurningKnight.entity.component;
using BurningKnight.entity.item;
using BurningKnight.save;
using BurningKnight.state;
using BurningKnight.ui.dialog;
using Lens.input;

namespace BurningKnight.entity.creature.player {
	public class ActiveWeaponComponent : WeaponComponent {
		private bool stopped = true;
		
		public ActiveWeaponComponent() {
			AtBack = false;
		}

		public override void Update(float dt) {
			base.Update(dt);

			var controller = GetComponent<GamepadComponent>().Controller;

			if (Item != null) {
				if (GetComponent<BuffsComponent>().Has<CharmedBuff>()) {
					return;
				}
				
				if (Input.WasPressed(Controls.Use, controller) || (Item.Automatic && Input.IsDown(Controls.Use, controller) && Item.Delay <= 0.001f)) {
					if (GlobalSave.IsFalse("control_use")) {
						GlobalSave.Put("control_use", true);
						GetComponent<DialogComponent>().Close();
					}
					
					Item.Use((Player) Entity);
				}
			}
	
			if ((Input.WasPressed(Controls.Swap, controller) || (Input.Mouse.WheelDelta != 0 && stopped)) && Run.Depth > 0) {
				stopped = false;
				Swap();
				Entity.GetComponent<AudioEmitterComponent>().EmitRandomized("swap");
			}

			stopped = Input.Mouse.WheelDelta == 0;
		}

		protected override bool ShouldReplace(Item item) {
			return base.ShouldReplace(item) && (Item == null || Run.Depth < 1 || Entity.GetComponent<WeaponComponent>().Item != null);
		}

		protected override void OnItemSet() {
			base.OnItemSet();

			if (GlobalSave.IsFalse("control_use")) {
				GetComponent<DialogComponent>().Start("control_2");
				GetComponent<DialogComponent>().Dialog.Str.SetVariable("ctrl", Controls.Find(Controls.Use, GamepadComponent.Current != null));
			}
		}
	}
}