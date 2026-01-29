from ..common import OUTWARD
from test.bases import WorldTestBase

class OutwardWorldTest(WorldTestBase):
    game = OUTWARD

    def test_fill(self):
        """Test that the world generates successfully."""
        self.world_setup()
        print("World generated successfully!")
