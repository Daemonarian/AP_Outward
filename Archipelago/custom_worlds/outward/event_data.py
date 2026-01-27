from typing import List, NamedTuple

class EventName:
    MAIN_QUEST_01_COMPLETE = 'Event - Main Quest 1 - Complete'
    MAIN_QUEST_02_COMPLETE = 'Event - Main Quest 2 - Complete'
    MAIN_QUEST_03_COMPLETE = 'Event - Main Quest 3 - Complete'
    MAIN_QUEST_04_COMPLETE = 'Event - Main Quest 4 - Complete'
    MAIN_QUEST_05_COMPLETE = 'Event - Main Quest 5 - Complete'
    MAIN_QUEST_06_COMPLETE = 'Event - Main Quest 6 - Complete'
    MAIN_QUEST_07_COMPLETE = 'Event - Main Quest 7 - Complete'
    MAIN_QUEST_08_COMPLETE = 'Event - Main Quest 8 - Complete'
    MAIN_QUEST_09_COMPLETE = 'Event - Main Quest 9 - Complete'
    MAIN_QUEST_10_COMPLETE = 'Event - Main Quest 10 - Complete'
    MAIN_QUEST_11_COMPLETE = 'Event - Main Quest 11 - Complete'
    MAIN_QUEST_12_COMPLETE = 'Event - Main Quest 12 - Complete'

class EventData(NamedTuple):
    code: int
    name: str

all_event_data: List[EventData] = [EventData(*data) for data in [
    (100, EventName.MAIN_QUEST_01_COMPLETE),
    (101, EventName.MAIN_QUEST_02_COMPLETE),
    (102, EventName.MAIN_QUEST_03_COMPLETE),
    (103, EventName.MAIN_QUEST_04_COMPLETE),
    (104, EventName.MAIN_QUEST_05_COMPLETE),
    (105, EventName.MAIN_QUEST_06_COMPLETE),
    (106, EventName.MAIN_QUEST_07_COMPLETE),
    (107, EventName.MAIN_QUEST_08_COMPLETE),
    (108, EventName.MAIN_QUEST_09_COMPLETE),
    (109, EventName.MAIN_QUEST_10_COMPLETE),
    (110, EventName.MAIN_QUEST_11_COMPLETE),
    (111, EventName.MAIN_QUEST_12_COMPLETE),
]]
