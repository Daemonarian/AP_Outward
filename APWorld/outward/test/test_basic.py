from . import OutwardWorldTestBase

from ..items import OutwardItemName
from ..locations import OutwardLocationName
from ..events import OutwardEventName

class TestBasic(OutwardWorldTestBase):
    def test_main_quest_line(self):
        """
        Test that the accessibility of main quests is as expected.
        """

        quest_licenses = [item for item in self.multiworld.itempool if item.name == OutwardItemName.QUEST_LICENSE]
        self.assertEqual(len(quest_licenses), 10)
        
        self.complete_by_name(OutwardEventName.MAIN_QUEST_01_PREREQ)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_01_COMPLETE))
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
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_05_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_06_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_07_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_08_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_09_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_10_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_11_COMPLETE))
        self.assertFalse(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))
        
        self.collect_single_by_name(OutwardItemName.QUEST_LICENSE)
        self.assertTrue(self.can_reach_location(OutwardEventName.MAIN_QUEST_12_COMPLETE))

