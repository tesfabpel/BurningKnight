using BurningKnight.entity.room.controllable.spikes;
using BurningKnight.level.tile;
using Lens.util.math;

namespace BurningKnight.level.rooms.connection {
	public class TunnelRoom : ConnectionRoom {
		protected void Fill(Level Level) {
			if (Random.Chance()) {
				Painter.Rect(Level, Left, Top, GetWidth() - 1, GetHeight() - 1, Tile.WallA);
			} else {
				Painter.Fill(Level, this, Tile.WallA);
			}
		}

		public override void Paint(Level Level) {
			Fill(Level);
			var Fl = Random.Chance(25) ? Random.Chance(33) ? Tile.Chasm 
				: Random.Chance() ? Tiles.RandomWall() : Tile.Lava : Tiles.RandomFloorOrSpike();

			var w = GenerateSpot();
			
			if (GetWidth() > 4 && GetHeight() > 4 && Random.Chance()) {
				PaintTunnel(Level, Fl, w, true);
			}

			if (Fl == Tile.Lava) {
				PaintTunnel(Level, Tiles.RandomFloorOrSpike(), w);
			}

			if (Random.Chance()) {
				PaintTunnel(Level, Tiles.RandomFloorOrSpike(), w, true);
			}

			if (Random.Chance()) {
				PaintTunnel(Level, Fl.Matches(Tile.Dirt, Tile.Lava) ? Random.Chance() ? 
					Tile.Water : Tile.Dirt : Tiles.RandomFloorOrSpike(), GenerateSpot());
			}
			
			PaintTunnel(Level, Fl.Matches(Tile.Dirt, Tile.Lava) ? Random.Chance() ? 
				Tile.Water : Tile.Dirt : Tiles.RandomFloorOrSpike(), w);
		}
	}
}