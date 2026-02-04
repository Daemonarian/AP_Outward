from . import OutwardWorldTestBase

from ..items import OutwardItemName
from ..locations import OutwardLocationName
from ..events import OutwardEventName

class TestBasic(OutwardWorldTestBase):
    def test_item_count_equals_location_count(self):
        """
        Test that the number of created items matches the number of non-event locations.
        """

        locations = self.multiworld.get_locations(self.player)
        non_event_locations = [loc for loc in locations if not loc.is_event]
        items = self.multiworld.itempool
        self.assertEqual(len(non_event_locations), len(items), msg=f"Item count mismatch! Locations {len(non_event_locations)}, Items: {len(items)}")

    def test_main_quest_line(self):
        """
        Test that the accessibility of main quests is as expected.
        """

        quest_licenses = [item for item in self.multiworld.itempool if item.name == OutwardItemName.QUEST_LICENSE]
        self.assertEqual(len(quest_licenses), 10)

        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_01_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_02_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_03_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_04_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))

        self.complete_by_name(OutwardEventName.MAIN_QUEST_01_COMPLETE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_02_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_03_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_04_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_02_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_03_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_04_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_03_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_04_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_05_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_06_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_07_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_08_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_09_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_10_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_11_COMPLETE)
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))

