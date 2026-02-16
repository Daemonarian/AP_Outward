"""
Define some types and utilities for working with Factions in Outward.
"""

from enum import IntFlag

class OutwardFaction(IntFlag):
    """
    Flags to specify any subset of Outward factions.
    """

    BlueChamber = 0b0001
    HeroicKingdom = 0b0010
    HolyMission = 0b0100
    SoroborAcademy = 0b1000

    NoFaction = 0b0000
    OriginalThree = 0b0111
    AllFactions = 0b1111

outward_factions = [
    OutwardFaction.BlueChamber,
    OutwardFaction.HeroicKingdom,
    OutwardFaction.HolyMission,
    OutwardFaction.SoroborAcademy,
]
