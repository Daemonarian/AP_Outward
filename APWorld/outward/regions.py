"""
Contains information about all the regions in Outward and their connections.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Entrance, Region
from worlds.generic.Rules import add_rule

from .templates import OutwardObjectNamespace, OutwardObjectTemplate

if TYPE_CHECKING:
    from BaseClasses import MultiWorld
    from worlds.generic.Rules import CollectionRule

    from . import OutwardWorld

class OutwardRegion(Region):
    def __init__(self, name: str, multiworld: MultiWorld, player: int):
        template = OutwardRegionTemplate.get_template(name)
        super().__init__(template.name, player, multiworld, template.hint)

    def add_to_world(self, world: OutwardWorld) -> None:
        world.multiworld.regions.append(self)

class OutwardEntrance(Entrance):
    def __init__(self, name: str, player: int):
        template = OutwardEntranceTemplate.get_template(name)
        super().__init__(player, template.name)

    def add_to_world(self, world: OutwardWorld):
        template = OutwardEntranceTemplate.get_template(self.name)
        parent = world.get_region(template.from_region)
        self.parent_region = parent
        parent.exits.append(self)
        self.connect(world.get_region(template.to_region))
        
    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        add_rule(self, rule, combine)

class OutwardRegionTemplate(OutwardObjectTemplate):
    _hint: str | None

    def __init__(self, name: str, hint: str | None = None):
        super().__init__(name)
        self._hint = hint

    @property
    def hint(self) -> str | None:
        return self._hint

class OutwardEntranceTemplate(OutwardObjectTemplate):
    _from_region: str
    _to_region: str

    def __init__(self, name: str, from_region: str, to_region: str):
        super().__init__(name)
        self._from_region = from_region
        self._to_region = to_region

    @property
    def from_region(self) -> str:
        return self._from_region

    @property
    def to_region(self) -> str:
        return self._to_region

region = OutwardRegionTemplate.register_template
class OutwardRegionName(OutwardObjectNamespace):
    template = OutwardRegionTemplate

    MENU = region("Menu")
    UNPLACED = region("Game Area")

    # Chersonese

    CHERSONESE = region("Chersonese")
    CHERSONESE_NE = region("North-Eastern Chersonese (Beyond Ghost Pass)")

    CIERZO = region("Cierzo")
    CIERZO_STORAGE = region("Cierzo Storage")
    CIERZO_STORAGE_INSIDE_BACK_DOOR = region("Cierzo Storage - Inside Back Door")

    CONFLUX_CHAMBERS = region("Conflux Chambers")
    CONFLUX_PATH_BC = region("Conflux Path - Blue Chamber")
    CONFLUX_PATH_BC_INSIDE_FRONT_DOOR = region("Conflux Path - Blue Chamber - Inside Front Door")
    CONFLUX_PATH_HK = region("Conflux Path - Heroic Kingdom")
    CONFLUX_PATH_HM = region("Conflux Path - Holy Mission")
    CONFLUX_PATH_HM_OUTSIDE_BACK_DOOR = region("Conflux Path - Holy Mission - Outside Back Door")
    BLISTER_BURROW = region("Blister Burrow")
    CORRUPTED_TOMBS = region("Corrupted Tombs")
    GHOST_PASS = region("Ghost Pass")
    MONTCALM_CLAN_FORT = region("Montcalm Clan Fort")
    VENDAVEL_FORTRESS = region("Vendavel Fortress")
    VIGIL_TOMB = region("Vigil Tomb")
    VOLTAIC_HATCHERY = region("Voltaic Hatcher")

    BANDITS_PRISON = region("Bandit's Prison")
    HERMITS_HOUSE = region("Hermit's House")
    HYENA_BURROW = region("Hyena Burrow")
    IMMACULATES_CAMP_CHERSONESE = region("Immaculate's Camp - Chersonese")
    MANSIONS_CELLAR = region("Mansion's Cellar")
    PIRATES_HIDEOUT = region("Pirate's Hideout")
    STARFISH_CAVE = region("Starfish Cave")
    TROG_INFILTRATION = region("Trog Infiltration")

    # Enermerkar Forest

    ENMERKAR_FOREST = region("Enmerkar Forest")

    BERG = region("Berg")
    NECROPOLIS = region("Necropolis - Berg")

    ANCESTORS_RESTING_PLACE_ENMERKAR_FOREST_SIDE = region("Ancestor's Resting Place - Enmerkar Forst Side")
    ANCESTORS_RESTING_PLACE_NECROPOLIS_SIDE = region("Ancestor's Resting Place - Necropolis Side")
    CABAL_OF_WIND_TEMPLE = region("Cabal of Wind Temple")
    FACE_OF_THE_ANCIENTS = region("Face of the Ancients")
    FOREST_HIVES = region("Forest Hives")
    FOREST_HIVES_EXTERIOR = region("Forest Hives - Exterior")
    ROYAL_MANTICORES_LAIR = region("Royal Manticore's Lair")
    VIGIL_PYLON_ENMERKAR_FOREST = region("Vigil Pylon - Enmerkar Forest")

    BURNT_OUTPOST = region("Burnt Outpost")
    IMMACULATES_CAMP_ENMERKAR_FOREST = region("Immaculate's Camp - Enmerkar Forest")
    TREE_HUSK = region("Tree Husk")
    DOLMEN_CRYPT = region("Dolmen Crypt")
    DAMP_HUNTERS_CABIN = region("Damp Hunter's Cabin")
    OLD_HUNTERS_CABIN = region("Old Hunter's Cabin")
    WORN_HUNTERS_CABIN = region("Worn Hunter's Cabin")

    # Abrassar

    ABRASSAR = region("Abrassar")

    LEVANT = region("Levant")
    LEVANT_SLUMS = region("Levant Slums")

    ANCIENT_HIVE = region("Ancient Hive")
    ELECTRIC_LAB = region("Electric Lab")
    ELECTRIC_LAB_EXTERIOR = region("Electric Lab - Exterior")
    RUINS_OF_OLD_LEVANT = region("Ruins of Old Levant")
    SAND_ROSE_CAVE = region("Sand Rose Cave")
    STONE_TITAN_CAVES = region("Stone Titan Caves")
    SLIDE = region("The Slide")
    UNDERCITY_PASSAGE = region("Undercity Passage")

    CABAL_OF_WIND_OUTPOST = region("Cabal of Wind Outpost")
    CORSAIRS_HEADQUARTERS = region("Corsairs' Headquarters")
    DOCKS_STORAGE = region("Dock's Storage")
    IMMACULATES_CAMP_ABRASSAR = region("Immaculate's Camp - Abrassar")
    HIVE_PRISON = region("Hive Prison")
    CAPTAINS_CABIN = region("Parched Shipwrecks - Captain's Cabin")
    RIVERS_END = region("River's End")
    RUINED_OUTPOST = region("Ruined Outpost")

    # Hallowed Marsh

    HALLOWED_MARSH = region("Hallowed Marsh")
    HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST = region("Hallowed Marsh - Outside Enmerkar Forset")

    MONSOON = region("Monsoon")

    GIANTS_VILLAGE = region("Giants' Village")
    DARK_ZIGGURAT = region("Dark Ziggurat")
    DEAD_ROOTS = region("Dead Roots")
    DEAD_TREE = region("Dead Tree")
    JADE_QUARRY = region("Jade Quarry")
    REPTILIAN_LAIR = region("Reptilian Lair")
    SPIRE_OF_LIGHT = region("Spire of Light")
    ZIGGURAT_PASSAGE = region("Ziggurat Passage")

    ABANDONED_SHED = region("Abandoned Shed")
    ABANDONED_ZIGGURAT = region("Abandoned Ziggurat")
    FLOODED_CELLAR = region("Flooded Cellar")
    HOLLOWED_LOTUS = region("Hollowed Lotus")
    IMMACULATES_CAMP_HALLOWED_MARSH = region("Immaculate's Camp - Hallowed Marsh")
    STEAKOSAURS_BURROW = region("Steakosaur's Burrow")
    UNDER_ISLAND = region("Under Island")

    # Antique Plateau

    ANTIQUE_PLATEAU = region("Antique Plateau")

    HARMATTAN = region("Harmattan")

    ABANDONED_LIVING_QUARTERS = region("Abandoned Living Quarters")
    FORGOTTEN_RESEARCH_LABORATORY = region("Forgotten Research Laboratory")
    ANCIENT_FOUNDRY = region("Ancient Foundry")
    RUINED_WAREHOUSE = region("Ruined Warehouse")
    LOST_GOLEM_MANUFACTURING_FACILITY = region("Lost Golem Manufacturing Facility")
    CRUMBLING_LOADING_DOCKS = region("Crumbling Loading Docks")
    DESTROYED_TEST_CHAMBERS = region("Destroyed Test Chambers")
    COMPROMISED_MANA_TRANSFER_STATION = region("Compromised Mana Transfer Station")
    RUNIC_TRAIN = region("Runic Train")
    OLD_HARMATTAN = region("Old Harmattan")

    ABANDONED_STORAGE = region("Abandoned Storage")
    BANDIT_HIDEOUT = region("Bandit Hideout")
    BLOOD_MAGE_HIDEOUT = region("Blood Mage Hideout")
    CORRUPTED_CAVE = region("Corrupted Cave")
    IMMACULATES_CAMP_ANTIQUE_PLATEAU = region("Immaculate's Camp - Antique Plateau")
    OLD_HARMATTAN_BASEMENT = region("Old Harmattan Basement")
    TROGLODYTE_WARREN = region("Troglodyte Warren")
    WENDIGO_LAIR = region("Wendigo Lair")

    # Caldera

    CALDERA = region("Caldera")

    NEW_SIROCCO = region("New Sirocco")
    NEW_SIROCCO_MINES = region("New Sirocco Mines")

    ARK_OF_THE_EXILED = region("Ark of the Exiled")
    ARK_OF_THE_EXILED_UPPER = region("Ark of the Exiled - Upper Area")
    ARK_OF_THE_EXILED_LOWER = region("Ark of the Exiled - Lower Area")
    MYRMITAURS_HAVEN = region("Myrmitaur's Haven")
    MYRMITAURS_HAVEN_EXTERIOR = region("Myrmitaur's Haven - Exterior")
    OIL_REFINERY = region("Oil Refinery")
    OLD_SIROCCO = region("Old Sirocco")
    ELDEST_BROTHER = region("The Eldest Brother")
    SCARLET_SANCTUARY = region("Scarlet Sanctuary")
    STEAM_BATH_TUNNELS = region("Steam Bath Tunnels")
    SULPHURIC_CAVERNS = region("Sulphuric Caverns")
    GROTTO_OF_CHALCEDONY = region("The Grotto of Chalcedony")
    VAULT_OF_STONE = region("The Vault of Stone")

    CALYGREY_COLOSSEUM = region("Calygrey Colosseum")
    GIANTS_SAUNA = region("Giant's Sauna")
    IMMACULATES_CAMP_CALDERA = region("Immaculate's Camp - Caldera")
    OILY_CAVERN = region("Oily Cavern")
    RITUALISTS_HUT = region("Ritualist's Hut")
    SILKWORMS_REFUGE = region("Silkworm's Refuge")
    RIVER_OF_RED = region("The River of Red")
    TOWER_OF_REGRETS = region("The Tower of Regrets")
    UNDERSIDE_LOADING_DOCK = region("Underside Loading Dock")

    # other

    LEYLINE_ANY = region("Leyline - Either")
    
entrance = OutwardEntranceTemplate.register_template
class OutwardEntranceName(OutwardObjectNamespace):
    template = OutwardEntranceTemplate

    # start of game

    UNPLACED = entrance("Menu to Unplaced", OutwardRegionName.MENU, OutwardRegionName.UNPLACED)
    ENTER_GAME = entrance("Enter Game", OutwardRegionName.MENU, OutwardRegionName.CIERZO)

    # major travel routes

    TRAVEL_CHERSONESE_TO_HALLOWED_MARSH = entrance("Travel - Chersonese to Hallowed Marsh", OutwardRegionName.CHERSONESE, OutwardRegionName.HALLOWED_MARSH)
    TRAVEL_CHERSONESE_TO_ENMERKAR_FOREST = entrance("Travel - Chersonese to Enmerkar Forest", OutwardRegionName.CHERSONESE, OutwardRegionName.ENMERKAR_FOREST)

    TRAVEL_ENMERKAR_FOREST_TO_CHERSONESE = entrance("Travel - Enmerkar Forest to Chersonese", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.CHERSONESE)
    TRAVEL_ENMERKAR_FOREST_TO_HALLOWED_MARSH = entrance("Travel - Enmerkar Forest to Hallowed Marsh", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST)
    TRAVEL_ENMERKAR_FOREST_TO_ABRASSAR = entrance("Travel - Enmerkar Forest to Abrassar", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.ABRASSAR)
    TRAVEL_ENMERKAR_FOREST_TO_CALDERA = entrance("Travel - Enmerkar Forest to Caldera", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.CALDERA)
    
    TRAVEL_ABRASSAR_TO_ENMERKAR_FOREST = entrance("Travel - Abrassar to Enmerkar Forest", OutwardRegionName.ABRASSAR, OutwardRegionName.ENMERKAR_FOREST)

    TRAVEL_HALLOWED_MARSH_TO_CHERSONESE = entrance("Travel - Hallowed Marsh to Chersonese", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.CHERSONESE)
    TRAVEL_HALLOWED_MARSH_TO_ENMERKAR_FOREST = entrance("Travel - Hallowed Marsh to Enmerkar Forest", OutwardRegionName.HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST, OutwardRegionName.ENMERKAR_FOREST)

    TRAVEL_CALDERA_TO_ENMERKAR_FOREST = entrance("Travel - Caldera to Enmerkar Forest", OutwardRegionName.CALDERA, OutwardRegionName.ENMERKAR_FOREST)

    # caravan

    CARAVAN_CIERZO_TO_HARMATTAN = entrance("Caravan - Cierzo to Harmattan", OutwardRegionName.CIERZO, OutwardRegionName.HARMATTAN)
    CARAVAN_BERG_TO_HARMATTAN = entrance("Caravan - Enmerkar to Harmattan", OutwardRegionName.BERG, OutwardRegionName.HARMATTAN)
    CARAVAN_LEVANT_TO_HARMATTAN = entrance("Caravan - Levant to Harmattan", OutwardRegionName.LEVANT, OutwardRegionName.HARMATTAN)
    CARAVAN_MONSOON_TO_HARMATTAN = entrance("Caravan - Monsoon to Harmattan", OutwardRegionName.MONSOON, OutwardRegionName.HARMATTAN)
    CARAVAN_NEW_SIROCCO_TO_HARMATTAN = entrance("Caravan - New Sirocco to Harmattan", OutwardRegionName.NEW_SIROCCO, OutwardRegionName.HARMATTAN)
    
    CARAVAN_HARMATTAN_TO_CIERZO = entrance("Caravan - Harmattan to Cierzo", OutwardRegionName.HARMATTAN, OutwardRegionName.CIERZO)
    CARAVAN_HARMATTAN_TO_BERG = entrance("Caravan - Harmattan to Berg", OutwardRegionName.HARMATTAN, OutwardRegionName.BERG)
    CARAVAN_HARMATTAN_TO_LEVANT = entrance("Caravan - Harmattan to Levant", OutwardRegionName.HARMATTAN, OutwardRegionName.LEVANT)
    CARAVAN_HARMATTAN_TO_MONSOON = entrance("Caravan - Harmattan to Monsoon", OutwardRegionName.HARMATTAN, OutwardRegionName.MONSOON)
    CARAVAN_HARMATTAN_TO_NEW_SIROCCO = entrance("Caravan - Harmattan to New Sirocco", OutwardRegionName.HARMATTAN, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_CHERSONESE_TO_BERG = entrance("Caravan - Chersonese to Berg", OutwardRegionName.CHERSONESE, OutwardRegionName.BERG)
    CARAVAN_CHERSONESE_TO_LEVANT = entrance("Caravan - Chersonese to Levant", OutwardRegionName.CHERSONESE, OutwardRegionName.LEVANT)
    CARAVAN_CHERSONESE_TO_MONSOON = entrance("Caravan - Chersonese to Monsoon", OutwardRegionName.CHERSONESE, OutwardRegionName.MONSOON)
    CARAVAN_CHERSONESE_TO_HARMATTAN = entrance("Caravan - Chersonese to Harmattan", OutwardRegionName.CHERSONESE, OutwardRegionName.HARMATTAN)
    CARAVAN_CHERSONESE_TO_NEW_SIROCCO = entrance("Caravan - Chersonese to New Sirocco", OutwardRegionName.CHERSONESE, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_ENMERKAR_FOREST_TO_CIERZO = entrance("Caravan - Enmerkar Forest to Cierzo", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.CIERZO)
    CARAVAN_ENMERKAR_FOREST_TO_LEVANT = entrance("Caravan - Enmerkar Forest to Levant", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.LEVANT)
    CARAVAN_ENMERKAR_FOREST_TO_MONSOON = entrance("Caravan - Enmerkar Forest to Monsoon", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.MONSOON)
    CARAVAN_ENMERKAR_FOREST_TO_HARMATTAN = entrance("Caravan - Enmerkar Forest to Harmattan", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.HARMATTAN)
    CARAVAN_ENMERKAR_FOREST_TO_NEW_SIROCCO = entrance("Caravan - Enmerkar Forest to New Sirocco", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_ABRASSAR_TO_CIERZO = entrance("Caravan - Abrassar to Cierzo", OutwardRegionName.ABRASSAR, OutwardRegionName.CIERZO)
    CARAVAN_ABRASSAR_TO_BERG = entrance("Caravan - Abrassar to Berg", OutwardRegionName.ABRASSAR, OutwardRegionName.BERG)
    CARAVAN_ABRASSAR_TO_MONSOON = entrance("Caravan - Abrassar to Monsoon", OutwardRegionName.ABRASSAR, OutwardRegionName.MONSOON)
    CARAVAN_ABRASSAR_TO_HARMATTAN = entrance("Caravan - Abrassar to Harmattan", OutwardRegionName.ABRASSAR, OutwardRegionName.HARMATTAN)
    CARAVAN_ABRASSAR_TO_NEW_SIROCCO = entrance("Caravan - Abrassar to New Sirocco", OutwardRegionName.ABRASSAR, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_HALLOWED_MARSH_TO_CIERZO = entrance("Caravan - Hallowed Marsh to Cierzo", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.CIERZO)
    CARAVAN_HALLOWED_MARSH_TO_BERG = entrance("Caravan - Hallowed Marsh to Berg", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.BERG)
    CARAVAN_HALLOWED_MARSH_TO_LEVANT = entrance("Caravan - Hallowed Marsh to Levant", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.LEVANT)
    CARAVAN_HALLOWED_MARSH_TO_HARMATTAN = entrance("Caravan - Hallowed Marsh to Harmattan", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.HARMATTAN)
    CARAVAN_HALLOWED_MARSH_TO_NEW_SIROCCO = entrance("Caravan - Hallowed Marsh to New Sirocco", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_ANTIQUE_PLATEAU_TO_CIERZO = entrance("Caravan - Antique Plateau to Cierzo", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.CIERZO)
    CARAVAN_ANTIQUE_PLATEAU_TO_BERG = entrance("Caravan - Antique Plateau to Berg", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.BERG)
    CARAVAN_ANTIQUE_PLATEAU_TO_LEVANT = entrance("Caravan - Antique Plateau to Levant", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.LEVANT)
    CARAVAN_ANTIQUE_PLATEAU_TO_MONSOON = entrance("Caravan - Antique Plateau to Monsoon", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.MONSOON)
    CARAVAN_ANTIQUE_PLATEAU_TO_NEW_SIROCCO = entrance("Caravan - Antique Plateau to New Sirocco", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.NEW_SIROCCO)
    
    CARAVAN_CALDERA_TO_CIERZO = entrance("Caravan - Caldera to Cierzo", OutwardRegionName.CALDERA, OutwardRegionName.CIERZO)
    CARAVAN_CALDERA_TO_BERG = entrance("Caravan - Caldera to Berg", OutwardRegionName.CALDERA, OutwardRegionName.BERG)
    CARAVAN_CALDERA_TO_LEVANT = entrance("Caravan - Caldera to Levant", OutwardRegionName.CALDERA, OutwardRegionName.LEVANT)
    CARAVAN_CALDERA_TO_MONSOON = entrance("Caravan - Caldera to Monsoon", OutwardRegionName.CALDERA, OutwardRegionName.MONSOON)
    CARAVAN_CALDERA_TO_HARMATTAN = entrance("Caravan - Caldera to Harmattan", OutwardRegionName.CALDERA, OutwardRegionName.HARMATTAN)

    # chersonese

    CIERZO_GATE_ENTER = entrance("Cierzo - Gate - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.CIERZO)
    CIERZO_STORAGE_BACK_DOOR_ENTER = entrance("Cierzo Storage - Back Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.CIERZO_STORAGE_INSIDE_BACK_DOOR)
    CONFLUX_PATH_BC_FRONT_DOOR_ENTER = entrance("Conflux Path - Blue Chamber - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.CONFLUX_PATH_BC_INSIDE_FRONT_DOOR)
    CONFLUX_PATH_HK_FRONT_DOOR_ENTER = entrance("Conflux Path - Heroic Kingdom - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.CONFLUX_PATH_HK)
    CONFLUX_PATH_HM_FRONT_DOOR_ENTER = entrance("Conflux Path - Holy Mission - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.CONFLUX_PATH_HM)
    BLISTER_BURROW_FRONT_DOOR_ENTER = entrance("Blister Burrow - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.BLISTER_BURROW)
    GHOST_PASS_SOUTH_DOOR_ENTER = entrance("Ghost Pass - South Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.GHOST_PASS)
    MONTCALM_CLAN_FORT_WEST_DOOR_ENTER = entrance("Montcalm Clan Fort - West Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.MONTCALM_CLAN_FORT)
    MONTCALM_CLAN_FORT_EAST_DOOR_ENTER = entrance("Montcalm Clan Fort - East Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.MONTCALM_CLAN_FORT)
    VENDAVEL_FORTRESS_FRONT_DOOR_ENTER = entrance("Vendavel Fortress - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.VENDAVEL_FORTRESS)
    VIGIL_TOMB_FRONT_DOOR_ENTER = entrance("Vigil Tomb - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.VIGIL_TOMB)
    VOLTAIC_HATCHERY_FRONT_DOOR_ENTER = entrance("Voltaic Hatchery - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.VOLTAIC_HATCHERY)
    BANDITS_PRISON_FRONT_DOOR_ENTER = entrance("Bandit's Prison - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.BANDITS_PRISON)
    HERMITS_HOUSE_FRONT_DOOR_ENTER = entrance("Hermit's House - Front Door - Enter", OutwardRegionName.CHERSONESE_NE, OutwardRegionName.HERMITS_HOUSE)
    HYENA_BURROW_FRONT_DOOR_ENTER = entrance("Hyena Burrow - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.HYENA_BURROW)
    IMMACULATES_CAMP_CHERSONESE_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Chersonese - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.IMMACULATES_CAMP_CHERSONESE)
    PIRATES_HIDEOUT_FRONT_DOOR_ENTER = entrance("Pirates' Hideout - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.PIRATES_HIDEOUT)
    STARFISH_CAVE_FRONT_DOOR_ENTER = entrance("Starfish Cave - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.STARFISH_CAVE)
    TROG_INFILTRATION_FRONT_DOOR_ENTER = entrance("Trog Infiltration - Front Door - Enter", OutwardRegionName.CHERSONESE, OutwardRegionName.TROG_INFILTRATION)

    CORRUPTED_TOMBS_FRONT_DOOR_ENTER = entrance("Corrupted Tombs - Front Door - Enter", OutwardRegionName.CHERSONESE_NE, OutwardRegionName.CORRUPTED_TOMBS)
    GHOST_PASS_NORTH_DOOR_ENTER = entrance("Ghost Pass - North Door - Enter", OutwardRegionName.CHERSONESE_NE, OutwardRegionName.GHOST_PASS)
    MANSIONS_CELLAR_FRONT_DOOR_ENTER = entrance("Mansion's Cellar - Front Door - Enter", OutwardRegionName.CHERSONESE_NE, OutwardRegionName.MANSIONS_CELLAR)

    CIERZO_GATE_EXIT = entrance("Cierzo - Gate - Exit", OutwardRegionName.CIERZO, OutwardRegionName.CHERSONESE)
    CIERZO_STORAGE_FRONT_DOOR_ENTER = entrance("Cierzo Storage - Front Door - Enter", OutwardRegionName.CIERZO, OutwardRegionName.CIERZO_STORAGE)

    CIERZO_STORAGE_FRONT_DOOR_EXIT = entrance("Cierzo Storage - Front Door - Exit", OutwardRegionName.CIERZO_STORAGE, OutwardRegionName.CIERZO)
    CIERZO_STORAGE_LEDGE = entrance("Cierzo Storage - Ledge", OutwardRegionName.CIERZO_STORAGE, OutwardRegionName.CIERZO_STORAGE_INSIDE_BACK_DOOR)
    CIERZO_STORAGE_BACK_DOOR_EXIT = entrance("Cierzo Storage - Back Door - Exit", OutwardRegionName.CIERZO_STORAGE_INSIDE_BACK_DOOR, OutwardRegionName.CHERSONESE)

    CONFLUX_PATH_BC_FRONT_DOOR_EXIT = entrance("Conflux Path - Blue Chamber - Front Door - Exit", OutwardRegionName.CONFLUX_PATH_BC_INSIDE_FRONT_DOOR, OutwardRegionName.CHERSONESE)
    CONFLUX_PATH_BC_LEDGE = entrance("Conflux Path - Blue Chamber - Ledge", OutwardRegionName.CONFLUX_PATH_BC_INSIDE_FRONT_DOOR, OutwardRegionName.CONFLUX_PATH_BC)
    CONFLUX_PATH_BC_BACK_DOOR_EXIT = entrance("Conflux Path - Blue Chamber - Back Door - Exit", OutwardRegionName.CONFLUX_PATH_BC, OutwardRegionName.CONFLUX_CHAMBERS)
    CONFLUX_PATH_HK_FRONT_DOOR_EXIT = entrance("Conflux Path - Heroic Kingdom - Front Door - Exit", OutwardRegionName.CONFLUX_PATH_HK, OutwardRegionName.CHERSONESE)
    CONFLUX_PATH_HK_BACK_DOOR_EXIT = entrance("Conflux Path - Heroic Kingdom - Back Door - Exit", OutwardRegionName.CONFLUX_PATH_HK, OutwardRegionName.CONFLUX_CHAMBERS)
    CONFLUX_PATH_HM_FRONT_DOOR_EXIT = entrance("Conflux Path - Holy Mission - Front Door - Exit", OutwardRegionName.CONFLUX_PATH_HM, OutwardRegionName.CHERSONESE)
    CONFLUX_PATH_HM_BACK_DOOR_EXIT = entrance("Conflux Path - Holy Mission - Back Door - Exit", OutwardRegionName.CONFLUX_PATH_HM, OutwardRegionName.CONFLUX_PATH_HM_OUTSIDE_BACK_DOOR)
    CONFLUX_PATH_HM_BACK_DOOR_ENTER = entrance("Conflux Path - Holy Mission - Back Door - Enter", OutwardRegionName.CONFLUX_PATH_HM_OUTSIDE_BACK_DOOR, OutwardRegionName.CONFLUX_PATH_HM)
    CONFLUX_CHAMBERS_LEDGE = entrance("Conflux Chambers - Ledge", OutwardRegionName.CONFLUX_PATH_HM_OUTSIDE_BACK_DOOR, OutwardRegionName.CONFLUX_CHAMBERS)
    CONFLUX_PATH_BC_BACK_DOOR_ENTER = entrance("Conflux Path - Blue Chamber - Back Door - Enter", OutwardRegionName.CONFLUX_CHAMBERS, OutwardRegionName.CONFLUX_PATH_BC)
    CONFLUX_PATH_HK_BACK_DOOR_ENTER = entrance("Conflux Path - Heroic Kingdom - Back Door - Enter", OutwardRegionName.CONFLUX_CHAMBERS, OutwardRegionName.CONFLUX_PATH_HK)
    CONFLUX_CHAMBERS_BACK_DOOR_EXIT = entrance("Conflux Chambers - Back Door - Exit", OutwardRegionName.CONFLUX_CHAMBERS, OutwardRegionName.CHERSONESE)

    CORRUPTED_TOMBS_FRONT_DOOR_EXIT = entrance("Corrupted Tombs - Front Door - Exit", OutwardRegionName.CORRUPTED_TOMBS, OutwardRegionName.CHERSONESE_NE)
    CORRUPTED_TOMBS_BACK_DOOR_EXIT = entrance("Corrupted Tombs - Back Door - Exit", OutwardRegionName.CORRUPTED_TOMBS, OutwardRegionName.CHERSONESE)

    GHOST_PASS_SOUTH_DOOR_EXIT = entrance("Ghost Pass - South Door - Exit", OutwardRegionName.GHOST_PASS, OutwardRegionName.CHERSONESE)
    GHOST_PASS_NORTH_DOOR_EXIT = entrance("Ghost Pass - North Door - Exit", OutwardRegionName.GHOST_PASS, OutwardRegionName.CHERSONESE_NE)

    MONTCALM_CLAN_FORT_WEST_DOOR_EXIT = entrance("Montcalm Clan Fort - West Door - Exit", OutwardRegionName.MONTCALM_CLAN_FORT, OutwardRegionName.CHERSONESE)
    MONTCALM_CLAN_FORT_EAST_DOOR_EXIT = entrance("Montcalm Clan Fort - East Door - Exit", OutwardRegionName.MONTCALM_CLAN_FORT, OutwardRegionName.CHERSONESE)

    VENDAVEL_FORTRESS_FRONT_DOOR_EXIT = entrance("Vendavel Fortress - Front Doot - Exit", OutwardRegionName.VENDAVEL_FORTRESS, OutwardRegionName.CHERSONESE)
    VENDAVEL_FORTRESS_HOLE = entrance("Vendavel Fortress - The Hole", OutwardRegionName.VENDAVEL_FORTRESS, OutwardRegionName.CHERSONESE)
    VENDAVEL_FORTRESS_PITFALL = entrance("Vendavel Fortress - Pitfall", OutwardRegionName.VENDAVEL_FORTRESS, OutwardRegionName.TROG_INFILTRATION)

    BLISTER_BURROW_FRONT_DOOR_EXIT = entrance("Blister Burrow - Front Door - Exit", OutwardRegionName.BLISTER_BURROW, OutwardRegionName.CHERSONESE)
    VIGIL_TOMB_FRONT_DOOR_EXIT = entrance("Vigil Tomb - Front Door - Exit", OutwardRegionName.VIGIL_TOMB, OutwardRegionName.CHERSONESE)
    VOLTAIC_HATCHERY_FRONT_DOOR_EXIT = entrance("Voltaic Hatchery - Front Door - Exit", OutwardRegionName.VOLTAIC_HATCHERY, OutwardRegionName.CHERSONESE)
    BANDITS_PRISON_FRONT_DOOR_EXIT = entrance("Bandit's Prison - Front Door - Exit", OutwardRegionName.BANDITS_PRISON, OutwardRegionName.CHERSONESE)
    HERMITS_HOUSE_FRONT_DOOR_EXIT = entrance("Hermit's House - Front Door - Exit", OutwardRegionName.HERMITS_HOUSE, OutwardRegionName.CHERSONESE_NE)
    HYENA_BURROW_FRONT_DOOR_EXIT = entrance("Hyena Burrow - Front Door - Exit", OutwardRegionName.HYENA_BURROW, OutwardRegionName.CHERSONESE)
    IMMACULATES_CAMP_CHERSONESE_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Chersonese - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_CHERSONESE, OutwardRegionName.CHERSONESE)
    MANSIONS_CELLAR_FRONT_DOOR_EXIT = entrance("Mansion's Cellar - Front Door - Exit", OutwardRegionName.MANSIONS_CELLAR, OutwardRegionName.CHERSONESE_NE)
    PIRATES_HIDEOUT_FRONT_DOOR_EXIT = entrance("Pirates' Hideout - Front Door - Exit", OutwardRegionName.PIRATES_HIDEOUT, OutwardRegionName.CHERSONESE)
    STARFISH_CAVE_FRONT_DOOR_EXIT = entrance("Starfish Cave - Front Door - Exit", OutwardRegionName.STARFISH_CAVE, OutwardRegionName.CHERSONESE)
    TROG_INFILTRATION_FRONT_DOOR_EXIT = entrance("Trog Infiltration - Front Door - Exit", OutwardRegionName.TROG_INFILTRATION, OutwardRegionName.CHERSONESE)
    
    # enmerkar

    BERG_WEST_GATE_ENTER = entrance("Berg - West Gate - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.BERG)
    BERG_EAST_GATE_ENTER = entrance("Berg - East Gate - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.BERG)
    BERG_SOUTH_GATE_ENTER = entrance("Berg - South Gate - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.BERG)
    ANCESTORS_RESTING_PLACE_FRONT_DOOR_ENTER = entrance("Ancestor's Resting Place - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.ANCESTORS_RESTING_PLACE_ENMERKAR_FOREST_SIDE)
    CABAL_OF_WIND_TEMPLE_FRONT_DOOR_ENTER = entrance("Cabal of Wind Temple - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.CABAL_OF_WIND_TEMPLE)
    CABAL_OF_WIND_TEMPLE_BACK_DOOR_ENTER = entrance("Cabal of Wind Temple - Back Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.CABAL_OF_WIND_TEMPLE)
    FACE_OF_THE_ANCIENTS_FRONT_DOOR_ENTER = entrance("Face of the Ancients - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.FACE_OF_THE_ANCIENTS)
    FOREST_HIVE_NORTH_DOOR_ENTER = entrance("Forest Hive - North Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.FOREST_HIVES)
    FOREST_HIVE_SOUTH_DOOR_ENTER = entrance("Forest Hive - South Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.FOREST_HIVES)
    ROYAL_MANTICORES_LAIR_FRONT_DOOR_ENTER = entrance("Royal Manticore's Lair - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.ROYAL_MANTICORES_LAIR)
    VIGIL_PYLON_ENMERKAR_FOREST_FRONT_DOOR_ENTER = entrance("Vigil Pylon - Enmerkar Forest - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.VIGIL_PYLON_ENMERKAR_FOREST)
    BURNT_OUTPOST_FRONT_DOOR_ENTER = entrance("Burnt Outpost - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.BURNT_OUTPOST)
    IMMACULATES_CAMP_ENMERKAR_FOREST_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Enmerkar Forest - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.IMMACULATES_CAMP_ENMERKAR_FOREST)
    TREE_HUSK_FRONT_DOOR_ENTER = entrance("Tree Husk - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.TREE_HUSK)
    DOLMEN_CRYPT_FRONT_DOOR_ENTER = entrance("Dolmen Crypt - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.DOLMEN_CRYPT)
    DAMP_HUNTERS_CABIN_FRONT_DOOR_ENTER = entrance("Damp Hunter's Cabin - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.DAMP_HUNTERS_CABIN)
    OLD_HUNTERS_CABIN_FRONT_DOOR_ENTER = entrance("Old Hunter's Cabin - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.OLD_HUNTERS_CABIN)
    WORN_HUNTERS_CABIN_FRONT_DOOR_ENTER = entrance("Worn Hunter's Cabin - Front Door - Enter", OutwardRegionName.ENMERKAR_FOREST, OutwardRegionName.WORN_HUNTERS_CABIN)
    
    BERG_WEST_GATE_EXIT = entrance("Berg - West Gate - Exit", OutwardRegionName.BERG, OutwardRegionName.ENMERKAR_FOREST)
    BERG_EAST_GATE_EXIT = entrance("Berg - East Gate - Exit", OutwardRegionName.BERG, OutwardRegionName.ENMERKAR_FOREST)
    BERG_SOUTH_GATE_EXIT = entrance("Berg - South Gate - Exit", OutwardRegionName.BERG, OutwardRegionName.ENMERKAR_FOREST)
    NECROPOLIS_FRONT_DOOR_ENTER = entrance("Necropolis - Front Door - Enter", OutwardRegionName.BERG, OutwardRegionName.NECROPOLIS)

    NECROPOLIS_FRONT_DOOR_EXIT = entrance("Necropolis - Front Door - Exit", OutwardRegionName.NECROPOLIS, OutwardRegionName.BERG)
    NECROPOLIS_BACK_DOOR_EXIT = entrance("Necropolis - Back Door - Exit", OutwardRegionName.NECROPOLIS, OutwardRegionName.ANCESTORS_RESTING_PLACE_NECROPOLIS_SIDE)

    ANCESTORS_RESTING_PLACE_FRONT_DOOR_EXIT = entrance("Ancestor's Resting Place - Front Door - Exit", OutwardRegionName.ANCESTORS_RESTING_PLACE_ENMERKAR_FOREST_SIDE, OutwardRegionName.ENMERKAR_FOREST)
    NECROPOLIS_BACK_DOOR_ENTER = entrance("Necropolis - Back Door - Enter", OutwardRegionName.ANCESTORS_RESTING_PLACE_NECROPOLIS_SIDE, OutwardRegionName.NECROPOLIS)
    ANCESTORS_RESTING_PLACE_LEDGE = entrance("Ancestor's Resting Place - Ledge", OutwardRegionName.ANCESTORS_RESTING_PLACE_NECROPOLIS_SIDE, OutwardRegionName.ANCESTORS_RESTING_PLACE_ENMERKAR_FOREST_SIDE)

    CABAL_OF_WIND_TEMPLE_FRONT_DOOR_EXIT = entrance("Cabal of Wind Temple - Front Door - Exit", OutwardRegionName.CABAL_OF_WIND_TEMPLE, OutwardRegionName.ENMERKAR_FOREST)
    CABAL_OF_WIND_TEMPLE_BACK_DOOR_EXIT = entrance("Cabal of Wind Temple - Back Door - Exit", OutwardRegionName.CABAL_OF_WIND_TEMPLE, OutwardRegionName.ENMERKAR_FOREST)

    FOREST_HIVE_NORTH_DOOR_EXIT = entrance("Forest Hive - North Door - Exit", OutwardRegionName.FOREST_HIVES, OutwardRegionName.ENMERKAR_FOREST)
    FOREST_HIVE_SOUTH_DOOR_EXIT = entrance("Forest Hive - South Door - Exit", OutwardRegionName.FOREST_HIVES, OutwardRegionName.ENMERKAR_FOREST)
    FOREST_HIVE_BACK_DOOR_EXIT = entrance("Forest Hive - Back Door - Exit", OutwardRegionName.FOREST_HIVES, OutwardRegionName.FOREST_HIVES_EXTERIOR)
    FOREST_HIVE_BACK_DOOR_ENTER = entrance("Forest Hive - Back Door - Enter", OutwardRegionName.FOREST_HIVES_EXTERIOR, OutwardRegionName.FOREST_HIVES)

    FACE_OF_THE_ANCIENTS_FRONT_DOOR_EXIT = entrance("Face of the Ancients - Front Door - Exit", OutwardRegionName.FACE_OF_THE_ANCIENTS, OutwardRegionName.ENMERKAR_FOREST)
    ROYAL_MANTICORES_LAIR_FRONT_DOOR_EXIT = entrance("Royal Manticore's Lair - Front Door - Exit", OutwardRegionName.ROYAL_MANTICORES_LAIR, OutwardRegionName.ENMERKAR_FOREST)
    VIGIL_PYLON_ENMERKAR_FOREST_FRONT_DOOR_EXIT = entrance("Vigil Pylon - Enmerkar Forest - Front Door - Exit", OutwardRegionName.VIGIL_PYLON_ENMERKAR_FOREST, OutwardRegionName.ENMERKAR_FOREST)
    BURNT_OUTPOST_FRONT_DOOR_EXIT = entrance("Burnt Outpost - Front Door - Exit", OutwardRegionName.BURNT_OUTPOST, OutwardRegionName.ENMERKAR_FOREST)
    IMMACULATES_CAMP_ENMERKAR_FOREST_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Enmerkar Forest - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_ENMERKAR_FOREST, OutwardRegionName.ENMERKAR_FOREST)
    TREE_HUSK_FRONT_DOOR_EXIT = entrance("Tree Husk - Front Door - Exit", OutwardRegionName.TREE_HUSK, OutwardRegionName.ENMERKAR_FOREST)
    DOLMEN_CRYPT_FRONT_DOOR_EXIT = entrance("Dolmen Crypt - Front Door - Exit", OutwardRegionName.DOLMEN_CRYPT, OutwardRegionName.ENMERKAR_FOREST)
    DAMP_HUNTERS_CABIN_FRONT_DOOR_EXIT = entrance("Damp Hunter's Cabin - Front Door - Exit", OutwardRegionName.DAMP_HUNTERS_CABIN, OutwardRegionName.ENMERKAR_FOREST)
    OLD_HUNTERS_CABIN_FRONT_DOOR_EXIT = entrance("Old Hunter's Cabin - Front Door - Exit", OutwardRegionName.OLD_HUNTERS_CABIN, OutwardRegionName.ENMERKAR_FOREST)
    WORN_HUNTERS_CABIN_FRONT_DOOR_EXIT = entrance("Worn Hunter's Cabin - Front Door - Exit", OutwardRegionName.WORN_HUNTERS_CABIN, OutwardRegionName.ENMERKAR_FOREST)

    # abrassar

    LEVANT_WEST_GATE_ENTER = entrance("Levant - West Gate - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.LEVANT)
    LEVANT_SLUMS_SOUTH_DOOR_ENTER = entrance("Levant Slums - South Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.LEVANT_SLUMS)
    ANCIENT_HIVE_FRONT_DOOR_ENTER = entrance("Ancient Hive - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.ANCIENT_HIVE)
    ELECTRIC_LAB_FRONT_DOOR_ENTER = entrance("Electric Lab - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.ELECTRIC_LAB)
    RUINS_OF_OLD_LEVANT_FRONT_DOOR_ENTER = entrance("Ruins of Old Levant - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.RUINS_OF_OLD_LEVANT)
    SAND_ROSE_CAVE_FRONT_DOOR_ENTER = entrance("Sand Rose Cave - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.SAND_ROSE_CAVE)
    STONE_TITAN_CAVES_FRONT_DOOR_ENTER = entrance("Stone Titan Caves - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.STONE_TITAN_CAVES)
    STONE_TITAN_CAVES_NW_DOOR_ENTER = entrance("Stone Titan Caves - North-West Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.STONE_TITAN_CAVES)
    STONE_TITAN_CAVES_EAST_DOOR_ENTER = entrance("Stone Titan Caves - East Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.STONE_TITAN_CAVES)
    SLIDE_WEST_DOOR_ENTER = entrance("The Slide - West Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.SLIDE)
    SLIDE_NORTH_DOOR_ENTER = entrance("The Slide - North Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.SLIDE)
    SLIDE_SW_DOOR_ENTER = entrance("The Slide - South-West Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.SLIDE)
    UNDERCITY_PASSAGE_FRONT_DOOR_ENTER = entrance("Undercity Passage - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.UNDERCITY_PASSAGE)
    CABAL_OF_WIND_OUTPOST_FRONT_DOOR_ENTER = entrance("Cabal of Wind Outpost - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.CABAL_OF_WIND_OUTPOST)
    DOCKS_STORAGE_FRONT_DOOR_ENTER = entrance("Dock's Storage - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.DOCKS_STORAGE)
    IMMACULATES_CAMP_ABRASSAR_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Abrassar - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.IMMACULATES_CAMP_ABRASSAR)
    HIVE_PRISON_FRONT_DOOR_ENTER = entrance("Hive Prison - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.HIVE_PRISON)
    CAPTAINS_CABIN_FRONT_DOOR_ENTER = entrance("Captain's Cabin - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.CAPTAINS_CABIN)
    RIVERS_END_FRONT_DOOR_ENTER = entrance("River's End - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.RIVERS_END)
    RUINED_OUTPOST_FRONT_DOOR_ENTER = entrance("Ruined Outpost - Front Door - Enter", OutwardRegionName.ABRASSAR, OutwardRegionName.RUINED_OUTPOST)

    LEVANT_WEST_GATE_EXIT = entrance("Levant - West Gate - Exit", OutwardRegionName.LEVANT, OutwardRegionName.ABRASSAR)
    LEVANT_SOUTH_GATE_EXIT = entrance("Levant - South Gate - Exit", OutwardRegionName.LEVANT, OutwardRegionName.LEVANT_SLUMS)
    LEVANT_SECRET_DOOR_EXIT = entrance("Levant - Secret Door - Exit", OutwardRegionName.LEVANT, OutwardRegionName.UNDERCITY_PASSAGE)
    LEVANT_SOUTH_GATE_ENTER = entrance("Levant - South Gate - Enter", OutwardRegionName.LEVANT_SLUMS, OutwardRegionName.LEVANT)
    LEVANT_SLUMS_SOUTH_DOOR_EXIT = entrance("Levant Slums - South Door - Exit", OutwardRegionName.LEVANT_SLUMS, OutwardRegionName.ABRASSAR)
    LEVANT_SLUMS_SECRET_DOOR_EXIT = entrance("Levant Slums - Secret Door - Exit", OutwardRegionName.LEVANT_SLUMS, OutwardRegionName.UNDERCITY_PASSAGE)

    ELECTRIC_LAB_FRONT_DOOR_EXIT = entrance("Electric Lab - Front Door - Exit", OutwardRegionName.ELECTRIC_LAB, OutwardRegionName.ABRASSAR)
    ELECTRIC_LAB_BACK_DOOR_EXIT = entrance("Electric Lab - Back Door - Exit", OutwardRegionName.ELECTRIC_LAB, OutwardRegionName.ELECTRIC_LAB_EXTERIOR)
    ELECTRIC_LAB_BACK_DOOR_ENTER = entrance("Electric Lab - Back Door - Enter", OutwardRegionName.ELECTRIC_LAB_EXTERIOR, OutwardRegionName.ELECTRIC_LAB)

    RUINS_OF_OLD_LEVANT_FRONT_DOOR_EXIT = entrance("Ruins of Old Levant - Front Door - Exit", OutwardRegionName.RUINS_OF_OLD_LEVANT, OutwardRegionName.ABRASSAR)
    RUINS_OF_OLD_LEVANT_BACK_DOOR_EXIT = entrance("Ruins of Old Levant - Back Door - Exit", OutwardRegionName.RUINS_OF_OLD_LEVANT, OutwardRegionName.SLIDE)
    CORSAIRS_HEADQUARTERS_FRONT_DOOR_ENTER = entrance("Corsair's Headquarters - Front Door - Enter", OutwardRegionName.RUINS_OF_OLD_LEVANT, OutwardRegionName.CORSAIRS_HEADQUARTERS)

    STONE_TITAN_CAVES_FRONT_DOOR_EXIT = entrance("Stone Titan Caves - Front Door - Exit", OutwardRegionName.STONE_TITAN_CAVES, OutwardRegionName.ABRASSAR)
    STONE_TITAN_CAVES_NW_DOOR_EXIT = entrance("Stone Titan Caves - North-West Door - Exit", OutwardRegionName.STONE_TITAN_CAVES, OutwardRegionName.ABRASSAR)
    STONE_TITAN_CAVES_EAST_DOOR_EXIT = entrance("Stone Titan Caves - East Door - Exit", OutwardRegionName.STONE_TITAN_CAVES, OutwardRegionName.ABRASSAR)

    SLIDE_WEST_DOOR_EXIT = entrance("The Slide - West Door - Exit", OutwardRegionName.SLIDE, OutwardRegionName.ABRASSAR)
    SLIDE_NORTH_DOOR_EXIT = entrance("The Slide - North Door - Exit", OutwardRegionName.SLIDE, OutwardRegionName.ABRASSAR)
    SLIDE_SW_DOOR_EXIT = entrance("The Slide - South-West Door - Exit", OutwardRegionName.SLIDE, OutwardRegionName.ABRASSAR)
    RUINS_OF_OLD_LEVANT_BACK_DOOR_ENTER = entrance("Ruins of Old Levant - Back Door - Enter", OutwardRegionName.SLIDE, OutwardRegionName.RUINS_OF_OLD_LEVANT)

    LEVANT_SLUMS_SECRET_DOOR_ENTER = entrance("Levant Slums - Secret Door - Enter", OutwardRegionName.UNDERCITY_PASSAGE, OutwardRegionName.LEVANT_SLUMS)
    LEVANT_SECRET_DOOR_ENTER = entrance("Levant - Secret Door - Enter", OutwardRegionName.UNDERCITY_PASSAGE, OutwardRegionName.LEVANT)
    UNDERCITY_PASSAGE_FRONT_DOOR_EXIT = entrance("Undercity Passage - Front Door - Exit", OutwardRegionName.UNDERCITY_PASSAGE, OutwardRegionName.ABRASSAR)

    ANCIENT_HIVE_FRONT_DOOR_EXIT = entrance("Ancient Hive - Front Door - Exit", OutwardRegionName.ANCIENT_HIVE, OutwardRegionName.ABRASSAR)
    SAND_ROSE_CAVE_FRONT_DOOR_EXIT = entrance("Sand Rose Cave - Front Door - Exit", OutwardRegionName.SAND_ROSE_CAVE, OutwardRegionName.ABRASSAR)
    CABAL_OF_WIND_OUTPOST_FRONT_DOOR_EXIT = entrance("Cabal of Wind Outpost - Front Door - Exit", OutwardRegionName.CABAL_OF_WIND_OUTPOST, OutwardRegionName.ABRASSAR)
    CORSAIRS_HEADQUARTERS_FRONT_DOOR_EXIT = entrance("Corsair's Headquarters - Front Door - Exit", OutwardRegionName.CORSAIRS_HEADQUARTERS, OutwardRegionName.RUINS_OF_OLD_LEVANT)
    DOCKS_STORAGE_FRONT_DOOR_EXIT = entrance("Dock's Storage - Front Door - Exit", OutwardRegionName.DOCKS_STORAGE, OutwardRegionName.ABRASSAR)
    IMMACULATES_CAMP_ABRASSAR_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Abrassar - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_ABRASSAR, OutwardRegionName.ABRASSAR)
    HIVE_PRISON_FRONT_DOOR_EXIT = entrance("Hive Prison - Front Door - Exit", OutwardRegionName.HIVE_PRISON, OutwardRegionName.ABRASSAR)
    CAPTAINS_CABIN_FRONT_DOOR_EXIT = entrance("Captain's Cabin - Front Door - Exit", OutwardRegionName.CAPTAINS_CABIN, OutwardRegionName.ABRASSAR)
    RIVERS_END_FRONT_DOOR_EXIT = entrance("River's End - Front Door - Exit", OutwardRegionName.RIVERS_END, OutwardRegionName.ABRASSAR)
    RUINED_OUTPOST_FRONT_DOOR_EXIT = entrance("Ruined Outpost - Front Door - Exit", OutwardRegionName.RUINED_OUTPOST, OutwardRegionName.ABRASSAR)

    # hallowed marsh

    MONSOON_GATE_ENTER = entrance("Monsoon - Gate - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.MONSOON)
    GIANTS_VILLAGE_GATE_ENTER = entrance("Giants' Village - Gate - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.GIANTS_VILLAGE)
    DARK_ZIGGURAT_FRONT_DOOR_ENTER = entrance("Dark Ziggurat - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.DARK_ZIGGURAT)
    DEAD_ROOTS_TOP_DOOR_ENTER = entrance("Dead Roots - Top Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.DEAD_ROOTS)
    DEAD_ROOTS_BOTTOM_DOOR_ENTER = entrance("Dead Roots - Bottom Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.DEAD_ROOTS)
    JADE_QUARRY_FRONT_DOOR_ENTER = entrance("Jade Quarry - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.JADE_QUARRY)
    REPTILIAN_LAIR_TOP_DOOR_ENTER = entrance("Reptilian Lair - Top Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.REPTILIAN_LAIR)
    REPTILIAN_LAIR_NORTH_DOOR_ENTER = entrance("Reptilian Lair - North Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.REPTILIAN_LAIR)
    SPIRE_OF_LIGHT_FRONT_DOOR_ENTER = entrance("Spire of Light - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.SPIRE_OF_LIGHT)
    ABANDONED_SHED_FRONT_DOOR_ENTER = entrance("Abandoned Shed - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.ABANDONED_SHED)
    ABANDONED_ZIGGURAT_FRONT_DOOR_ENTER = entrance("Abandoned Ziggurat - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.ABANDONED_ZIGGURAT)
    FLOODED_CELLAR_FRONT_DOOR_ENTER = entrance("Flooded Cellar - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.FLOODED_CELLAR)
    HOLLOWED_LOTUS_FRONT_DOOR_ENTER = entrance("Hollowed Lotus - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.HOLLOWED_LOTUS)
    IMMACULATES_CAMP_HALLOWED_MARSH_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Hallowed Marsh - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.IMMACULATES_CAMP_HALLOWED_MARSH)
    STEAKOSAURS_BURROW_FRONT_DOOR_ENTER = entrance("Steakosaur's Burrow - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.STEAKOSAURS_BURROW)
    UNDER_ISLAND_FRONT_DOOR_ENTER = entrance("Under Island - Front Door - Enter", OutwardRegionName.HALLOWED_MARSH, OutwardRegionName.UNDER_ISLAND)

    ZIGGURAT_PASSAGE_EAST_DOOR_ENTER = entrance("Ziggurat Passage - East Door - Enter", OutwardRegionName.HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST, OutwardRegionName.ZIGGURAT_PASSAGE)

    DEAD_ROOTS_TOP_DOOR_EXIT = entrance("Dead Roots - Top Door - Exit", OutwardRegionName.DEAD_ROOTS, OutwardRegionName.HALLOWED_MARSH)
    DEAD_ROOTS_BOTTOM_DOOR_EXIT = entrance("Dead Roots - Bottom Door - Exit", OutwardRegionName.DEAD_ROOTS, OutwardRegionName.HALLOWED_MARSH)
    DEAD_TREE_FRONT_DOOR_ENTER = entrance("Dead Tree - Front Door - Enter", OutwardRegionName.DEAD_ROOTS, OutwardRegionName.DEAD_TREE)
    DEAD_TREE_FRONT_DOOR_EXIT = entrance("Dead Tree - Front Door - Exit", OutwardRegionName.DEAD_TREE, OutwardRegionName.DEAD_ROOTS)

    REPTILIAN_LAIR_TOP_DOOR_EXIT = entrance("Reptilian Lair - Top Door - Exit", OutwardRegionName.REPTILIAN_LAIR, OutwardRegionName.HALLOWED_MARSH)
    REPTILIAN_LAIR_NORTH_DOOR_EXIT = entrance("Reptilian Lair - North Door - Exit", OutwardRegionName.REPTILIAN_LAIR, OutwardRegionName.HALLOWED_MARSH)
    REPTILIAN_LAIR_SOUTH_DOOR_EXIT = entrance("Reptilian Lair - South Door - Exit", OutwardRegionName.REPTILIAN_LAIR, OutwardRegionName.HALLOWED_MARSH)

    SPIRE_OF_LIGHT_FRONT_DOOR_EXIT = entrance("Spire of Light - Front Door - Exit", OutwardRegionName.SPIRE_OF_LIGHT, OutwardRegionName.HALLOWED_MARSH)
    SPIRE_OF_LIGHT_BACK_DOOR_EXIT = entrance("Spire of Light - Back Door - Exit", OutwardRegionName.SPIRE_OF_LIGHT, OutwardRegionName.HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST)

    ZIGGURAT_PASSAGE_WEST_DOOR_EXIT = entrance("Ziggurat Passage - West Door - Exit", OutwardRegionName.ZIGGURAT_PASSAGE, OutwardRegionName.HALLOWED_MARSH)
    ZIGGURAT_PASSAGE_EAST_DOOR_EXIT = entrance("Ziggurat Passage - East Door - Exit", OutwardRegionName.ZIGGURAT_PASSAGE, OutwardRegionName.HALLOWED_MARSH_OUTSIDE_ENMERKAR_FOREST)

    MONSOON_GATE_EXIT = entrance("Monsoon - Gate - Exit", OutwardRegionName.MONSOON, OutwardRegionName.HALLOWED_MARSH)
    GIANTS_VILLAGE_GATE_ENTER = entrance("Giants' Village - Gate - Exit", OutwardRegionName.GIANTS_VILLAGE, OutwardRegionName.HALLOWED_MARSH)
    DARK_ZIGGURAT_FRONT_DOOR_EXIT = entrance("Dark Ziggurat - Front Door - Exit", OutwardRegionName.DARK_ZIGGURAT, OutwardRegionName.HALLOWED_MARSH)
    JADE_QUARRY_FRONT_DOOR_EXIT = entrance("Jade Quarry - Front Door - Exit", OutwardRegionName.JADE_QUARRY, OutwardRegionName.HALLOWED_MARSH)
    ABANDONED_SHED_FRONT_DOOR_EXIT = entrance("Abandoned Shed - Front Door - Exit", OutwardRegionName.ABANDONED_SHED, OutwardRegionName.HALLOWED_MARSH)
    ABANDONED_ZIGGURAT_FRONT_DOOR_EXIT = entrance("Abandoned Ziggurat - Front Door - Exit", OutwardRegionName.ABANDONED_ZIGGURAT, OutwardRegionName.HALLOWED_MARSH)
    FLOODED_CELLAR_FRONT_DOOR_EXIT = entrance("Flooded Cellar - Front Door - Exit", OutwardRegionName.FLOODED_CELLAR, OutwardRegionName.HALLOWED_MARSH)
    HOLLOWED_LOTUS_FRONT_DOOR_EXIT = entrance("Hollowed Lotus - Front Door - Exit", OutwardRegionName.HOLLOWED_LOTUS, OutwardRegionName.HALLOWED_MARSH)
    IMMACULATES_CAMP_HALLOWED_MARSH_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Hallowed Marsh - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_HALLOWED_MARSH, OutwardRegionName.HALLOWED_MARSH)
    STEAKOSAURS_BURROW_FRONT_DOOR_EXIT = entrance("Steakosaur's Burrow - Front Door - Exit", OutwardRegionName.STEAKOSAURS_BURROW, OutwardRegionName.HALLOWED_MARSH)
    UNDER_ISLAND_FRONT_DOOR_EXIT = entrance("Under Island - Front Door - Exit", OutwardRegionName.UNDER_ISLAND, OutwardRegionName.HALLOWED_MARSH)

    # antique plateau

    HARMATTAN_SOUTH_GATE_ENTER = entrance("Harmattan - South Gate - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.HARMATTAN)
    HARMATTAN_NORTH_WEST_GATE_ENTER = entrance("Harmattan - North-West Gate - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.HARMATTAN)
    HARMATTAN_NORTH_EAST_GATE_ENTER = entrance("Harmattan - North-East Gate - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.HARMATTAN)
    ABANDONED_LIVING_QUARTERS_FRONT_DOOR_ENTER = entrance("Abandoned Living Quarters - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.ABANDONED_LIVING_QUARTERS)
    ANCIENT_FOUNDRY_FRONT_DOOR_ENTER = entrance("Ancient Foundry - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.ANCIENT_FOUNDRY)
    RUINED_WAREHOUSE_FRONT_DOOR_ENTER = entrance("Ruined Warehouse - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.RUINED_WAREHOUSE)
    LOST_GOLEM_MANUFACTURING_FACILITY_FRONT_DOOR_ENTER  = entrance("Lost Golem Manufacturing Facility - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.LOST_GOLEM_MANUFACTURING_FACILITY)
    CRUMBLING_LOADING_DOCKS_FRONT_DOOR_ENTER = entrance("Crumbling Loading Docks - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.CRUMBLING_LOADING_DOCKS)
    DESTROYED_TEST_CHAMBERS_FRONT_DOOR_ENTER = entrance("Destroyed Test Chambers - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.DESTROYED_TEST_CHAMBERS)
    ABANDONED_STORAGE_FRONT_DOOR_ENTER = entrance("Abandoned Storage - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.ABANDONED_STORAGE)
    BANDIT_HIDEOUT_FRONT_DOOR_ENTER = entrance("Bandit Hideout - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.BANDIT_HIDEOUT)
    BLOOD_MAGE_HIDEOUT_FRONT_DOOR_ENTER = entrance("Blood Mage Hideout - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.BLOOD_MAGE_HIDEOUT)
    CORRUPTED_CAVE_FRONT_DOOR_ENTER = entrance("Corrupted Cave - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.CORRUPTED_CAVE)
    IMMACULATES_CAMP_ANTIQUE_PLATEAU_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Antique Plateau - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.IMMACULATES_CAMP_ANTIQUE_PLATEAU)
    OLD_HARMATTAN_BASEMENT_FRONT_DOOR_ENTER = entrance("Old Harmattan Basement - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.OLD_HARMATTAN_BASEMENT)
    TROGLODYTE_WARREN_FRONT_DOOR_ENTER = entrance("Troglodyte Warren - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.TROGLODYTE_WARREN)
    WENDIGO_LAIR_FRONT_DOOR_ENTER = entrance("Wendigo Lair - Front Door - Enter", OutwardRegionName.ANTIQUE_PLATEAU, OutwardRegionName.WENDIGO_LAIR)
    
    HARMATTAN_SOUTH_GATE_EXIT = entrance("Harmattan - South Gate - Exit", OutwardRegionName.HARMATTAN, OutwardRegionName.ANTIQUE_PLATEAU)
    HARMATTAN_NORTH_WEST_GATE_EXIT = entrance("Harmattan - North-West Gate - Exit", OutwardRegionName.HARMATTAN, OutwardRegionName.ANTIQUE_PLATEAU)
    HARMATTAN_NORTH_EAST_GATE_EXIT = entrance("Harmattan - North-East Gate - Exit", OutwardRegionName.HARMATTAN, OutwardRegionName.ANTIQUE_PLATEAU)

    ABANDONED_LIVING_QUARTERS_FRONT_DOOR_EXIT = entrance("Abandoned Living Quarters - Front Door - Exit", OutwardRegionName.ABANDONED_LIVING_QUARTERS, OutwardRegionName.ANTIQUE_PLATEAU)
    ABANDONED_LIVING_QUARTERS_TRAIN_DOOR_EXIT = entrance("Abandoned Living Quarters - Train Door - Exit", OutwardRegionName.ABANDONED_LIVING_QUARTERS, OutwardRegionName.RUNIC_TRAIN)

    FORGOTTEN_RESEARCH_LABORATORY_SHORTCUT_EXIT = entrance("Forgotten Research Laboratory - Shortcut - Exit", OutwardRegionName.FORGOTTEN_RESEARCH_LABORATORY, OutwardRegionName.HARMATTAN)
    FORGOTTEN_RESEARCH_LABORATORY_TRAIN_DOOR_EXIT = entrance("Forgotten Research Laboratory - Train Door - Exit", OutwardRegionName.FORGOTTEN_RESEARCH_LABORATORY, OutwardRegionName.RUNIC_TRAIN)

    ANCIENT_FOUNDRY_FRONT_DOOR_EXIT = entrance("Ancient Foundry - Front Door - Exit", OutwardRegionName.ANCIENT_FOUNDRY, OutwardRegionName.ANTIQUE_PLATEAU)
    ANCIENT_FOUNDRY_TRAIN_DOOR_EXIT = entrance("Ancient Foundry - Train Door - Exit", OutwardRegionName.ANCIENT_FOUNDRY, OutwardRegionName.RUNIC_TRAIN)

    RUINED_WAREHOUSE_FRONT_DOOR_EXIT = entrance("Ruined Warehouse - Front Door - Exit", OutwardRegionName.RUINED_WAREHOUSE, OutwardRegionName.ANTIQUE_PLATEAU)
    RUINED_WAREHOUSE_TRAIN_DOOR_EXIT = entrance("Ruined Warehouse - Train Door - Exit", OutwardRegionName.RUINED_WAREHOUSE, OutwardRegionName.RUNIC_TRAIN)

    LOST_GOLEM_MANUFACTURING_FACILITY_FRONT_DOOR_EXIT = entrance("Lost Golem Manufacturing Facility - Front Door - Exit", OutwardRegionName.LOST_GOLEM_MANUFACTURING_FACILITY, OutwardRegionName.ANTIQUE_PLATEAU)
    LOST_GOLEM_MANUFACTURING_FACILITY_TRAIN_DOOR_EXIT = entrance("Lost Golem Manufacturing Facility - Train Door - Exit", OutwardRegionName.LOST_GOLEM_MANUFACTURING_FACILITY, OutwardRegionName.RUNIC_TRAIN)

    CRUMBLING_LOADING_DOCKS_FRONT_DOOR_EXIT = entrance("Crumbling Loading Docks - Front Door - Exit", OutwardRegionName.CRUMBLING_LOADING_DOCKS, OutwardRegionName.ANTIQUE_PLATEAU)
    CRUMBLING_LOADING_DOCKS_TRAIN_DOOR_EXIT = entrance("Crumbling Loading Docks - Train Door - Exit", OutwardRegionName.CRUMBLING_LOADING_DOCKS, OutwardRegionName.RUNIC_TRAIN)

    DESTROYED_TEST_CHAMBERS_FRONT_DOOR_EXIT = entrance("Destroyed Test Chambers - Front Door - Exit", OutwardRegionName.DESTROYED_TEST_CHAMBERS, OutwardRegionName.ANTIQUE_PLATEAU)
    DESTORYED_TEST_CHAMBERS_TRAIN_DOOR_EXIT = entrance("Destroyed Test Chambers - Train Door - Exit", OutwardRegionName.DESTROYED_TEST_CHAMBERS, OutwardRegionName.RUNIC_TRAIN)

    COMPROMISED_MANA_TRANSFER_STATION_FRONT_DOOR_EXIT = entrance("Compromised Mana Transfer Station - Front Door - Exit", OutwardRegionName.COMPROMISED_MANA_TRANSFER_STATION, OutwardRegionName.ANTIQUE_PLATEAU)
    COMPROMISED_MANA_TRANSFER_STATION_TRAIN_DOOR_EXIT = entrance("Compromised Mana Transfer Station - Train Door - Exit", OutwardRegionName.COMPROMISED_MANA_TRANSFER_STATION, OutwardRegionName.RUNIC_TRAIN)

    ABANDONED_LIVING_QUARTERS_TRAIN_DOOR_ENTER = entrance("Abandoned Living Quarters - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.ABANDONED_LIVING_QUARTERS)
    FORGOTTEN_RESEARCH_LABORATORY_TRAIN_DOOR_ENTER = entrance("Forgotten Research Laboratory - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.FORGOTTEN_RESEARCH_LABORATORY)
    ANCIENT_FOUNDRY_TRAIN_DOOR_ENTER = entrance("Ancient Foundry - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.ANCIENT_FOUNDRY)
    RUINED_WAREHOUSE_TRAIN_DOOR_ENTER = entrance("Ruined Warehouse - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.RUINED_WAREHOUSE)
    LOST_GOLEM_MANUFACTURING_FACILITY_TRAIN_DOOR_ENTER = entrance("Lost Golem Manufacturing Facility - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.LOST_GOLEM_MANUFACTURING_FACILITY)
    CRUMBLING_LOADING_DOCKS_TRAIN_DOOR_ENTER = entrance("Crumbling Loading Docks - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.CRUMBLING_LOADING_DOCKS)
    DESTROYED_TEST_CHAMBERS_TRAIN_DOOR_ENTER = entrance("Destroyed Test Chambers - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.DESTROYED_TEST_CHAMBERS)
    COMPROMISED_MANA_TRANSFER_STATION_TRAIN_DOOR_ENTER = entrance("Compromised Mana Transfer Station - Train Door - Enter", OutwardRegionName.RUNIC_TRAIN, OutwardRegionName.COMPROMISED_MANA_TRANSFER_STATION)

    OLD_HARMATTAN_BASEMENT_FRONT_DOOR_EXIT = entrance("Old Harmattan Basement - Front Door - Exit", OutwardRegionName.OLD_HARMATTAN_BASEMENT, OutwardRegionName.ANTIQUE_PLATEAU)
    OLD_HARMATTAN_BASEMENT_BACK_DOOR_EXIT = entrance("Old Harmattan Basement - Back Door - Exit", OutwardRegionName.OLD_HARMATTAN_BASEMENT, OutwardRegionName.OLD_HARMATTAN)

    ABANDONED_STORAGE_FRONT_DOOR_EXIT = entrance("Abandoned Storage - Front Door - Exit", OutwardRegionName.ABANDONED_STORAGE, OutwardRegionName.ANTIQUE_PLATEAU)
    BANDIT_HIDEOUT_FRONT_DOOR_EXIT = entrance("Bandit Hideout - Front Door - Exit", OutwardRegionName.BANDIT_HIDEOUT, OutwardRegionName.ANTIQUE_PLATEAU)
    BLOOD_MAGE_HIDEOUT_FRONT_DOOR_EXIT = entrance("Blood Mage Hideout - Front Door - Exit", OutwardRegionName.BLOOD_MAGE_HIDEOUT, OutwardRegionName.ANTIQUE_PLATEAU)
    CORRUPTED_CAVE_FRONT_DOOR_EXIT = entrance("Corrupted Cave - Front Door - Exit", OutwardRegionName.CORRUPTED_CAVE, OutwardRegionName.ANTIQUE_PLATEAU)
    IMMACULATES_CAMP_ANTIQUE_PLATEAU_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Antique Plateau - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_ANTIQUE_PLATEAU, OutwardRegionName.ANTIQUE_PLATEAU)
    TROGLODYTE_WARREN_FRONT_DOOR_EXIT = entrance("Troglodyte Warren - Front Door - Exit", OutwardRegionName.TROGLODYTE_WARREN, OutwardRegionName.ANTIQUE_PLATEAU)
    WENDIGO_LAIR_FRONT_DOOR_EXIT = entrance("Wendigo Lair - Front Door - Exit", OutwardRegionName.WENDIGO_LAIR, OutwardRegionName.ANTIQUE_PLATEAU)

    # caldera

    NEW_SIROCCO_FRONT_DOOR_ENTER = entrance("New Sirocco - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.NEW_SIROCCO)
    ARK_OF_THE_EXILED_FRONT_DOOR_ENTER = entrance("Ark of the Exiled - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.ARK_OF_THE_EXILED) # 6 different locations
    ARK_OF_THE_EXILED_UPPER_FRONT_DOOR_ENTER = entrance("Ark of the Exiled - Upper Area - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.ARK_OF_THE_EXILED_UPPER)
    MYRMITAURS_HAVEN_FRONT_DOOR_ENTER = entrance("Myrmitaur's Haven - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.MYRMITAURS_HAVEN)
    OIL_REFINERY_FRONT_DOOR_ENTER = entrance("Oil Refinery - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.OIL_REFINERY)
    OIL_REFINERY_OUTFLOW_ENTER = entrance("Oil Refinery - Outflow - Enter", OutwardRegionName.CALDERA, OutwardRegionName.OIL_REFINERY)
    OLD_SIROCCO_FRONT_DOOR_ENTER = entrance("Old Sirocco - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.OLD_SIROCCO)
    SCARLET_SANCTUARY_FRONT_DOOR_ENTER = entrance("Scarlet Sanctuary - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.SCARLET_SANCTUARY)
    STEAM_BATH_TUNNELS_FRONT_DOOR_ENTER = entrance("Steam Bath Tunnels - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.STEAM_BATH_TUNNELS)
    SULPHURIC_CAVERNS_FRONT_DOOR_ENTER = entrance("Sulphuric Caverns - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.SULPHURIC_CAVERNS)
    GROTTO_OF_CHALCEDONY_FRONT_DOOR_ENTER = entrance("The Grotto of Chalcedony - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.GROTTO_OF_CHALCEDONY)
    VAULT_OF_STONE_FROTN_DOOR_ENTER = entrance("The Vault of Stone - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.VAULT_OF_STONE)
    CALYGREY_COLOSSEUM_FRONT_DOOR_ENTER = entrance("Calygrey Colosseum - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.CALYGREY_COLOSSEUM)
    GIANTS_SAUNA_FRONT_DOOR_ENTER = entrance("Giant's Sauna - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.GIANTS_SAUNA)
    IMMACULATES_CAMP_CALDERA_FRONT_DOOR_ENTER = entrance("Immaculate's Camp - Caldera - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.IMMACULATES_CAMP_CALDERA)
    OILY_CAVERN_FRONT_DOOR_ENTER = entrance("Oily Cavern - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.OILY_CAVERN)
    RITUALISTS_HUT_FRONT_DOOR_ENTER = entrance("Ritualist's Hut - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.RITUALISTS_HUT)
    SILKWORMS_REFUGE_FRONT_DOOR_ENTER = entrance("Silkworm's Refuge - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.SILKWORMS_REFUGE)
    RIVER_OF_RED_FRONT_DOOR_ENTER = entrance("The River of Red - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.RIVER_OF_RED)
    TOWER_OF_REGRETS_FRONT_DOOR_ENTER = entrance("The Tower of Regrets - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.TOWER_OF_REGRETS)
    UNDERSIDE_LOADING_DOCK_FRONT_DOOR_ENTER = entrance("Underside Loading Dock - Front Door - Enter", OutwardRegionName.CALDERA, OutwardRegionName.UNDERSIDE_LOADING_DOCK)

    NEW_SIROCCO_FRONT_DOOR_EXIT = entrance("New Sirocco - Front Door - Exit", OutwardRegionName.NEW_SIROCCO, OutwardRegionName.CALDERA)
    NEW_SIROCCO_MINES_FRONT_DOOR_ENTER = entrance("New Sirocco Mines - Front Door - Enter", OutwardRegionName.NEW_SIROCCO, OutwardRegionName.NEW_SIROCCO_MINES)
    NEW_SIROCCO_MINES_FRONT_DOOR_EXIT = entrance("New Sirocco Mines - Front Door - Exit", OutwardRegionName.NEW_SIROCCO_MINES, OutwardRegionName.NEW_SIROCCO)

    ARK_OF_THE_EXILED_FRONT_DOOR_EXIT = entrance("Ark of the Exiled - Front Door - Exit", OutwardRegionName.ARK_OF_THE_EXILED, OutwardRegionName.CALDERA)
    ARK_OF_THE_EXILED_UPPER_FRONT_DOOR_EXIT = entrance("Ark of the Exiled - Upper Area - Front Door - Exit", OutwardRegionName.ARK_OF_THE_EXILED_UPPER, OutwardRegionName.CALDERA)
    ARK_OF_THE_EXILED_LOWER_FRONT_DOOR_EXIT = entrance("Ark of the Exiled - Lower Area - Front Door - Exit", OutwardRegionName.ARK_OF_THE_EXILED_LOWER, OutwardRegionName.CALDERA)

    MYRMITAURS_HAVEN_FRONT_DOOR_EXIT = entrance("Myrmitaur's Haven - Front Door - Exit", OutwardRegionName.MYRMITAURS_HAVEN, OutwardRegionName.CALDERA)
    MYRMITAURS_HAVEN_BACK_DOOR_EXIT = entrance("Myrmitaur's Haven - Back Door - Exit", OutwardRegionName.MYRMITAURS_HAVEN, OutwardRegionName.MYRMITAURS_HAVEN_EXTERIOR)
    MYRMITAURS_HAVEN_BACK_DOOR_ENTER = entrance("Myrmitaur's Haven - Back Door - Enter", OutwardRegionName.MYRMITAURS_HAVEN_EXTERIOR, OutwardRegionName.MYRMITAURS_HAVEN)

    OIL_REFINERY_FRONT_DOOR_EXIT = entrance("Oil Refinery - Front Door - Exit", OutwardRegionName.OIL_REFINERY, OutwardRegionName.CALDERA)
    OIL_REFINERY_OUTFLOW_EXIT = entrance("Oil Refinery - Outflow - Exit", OutwardRegionName.OIL_REFINERY, OutwardRegionName.CALDERA)

    OLD_SIROCCO_FRONT_DOOR_EXIT = entrance("Old Sirocco - Front Door - Exit", OutwardRegionName.OLD_SIROCCO, OutwardRegionName.CALDERA)
    ELDEST_BROTHER_FRONT_DOOR_ENTER = entrance("The Eldest Brother - Front Door - Enter", OutwardRegionName.OLD_SIROCCO, OutwardRegionName.ELDEST_BROTHER)
    ELDEST_BROTHER_FRONT_DOOR_EXIT = entrance("The Eldest Brother - Front Door - Exit", OutwardRegionName.ELDEST_BROTHER, OutwardRegionName.OLD_SIROCCO)

    ARK_OF_THE_EXILED_LOWER_FRONT_DOOR_ENTER = entrance("Ark of the Exiled - Lower Area - Front Door - Enter", OutwardRegionName.UNDERSIDE_LOADING_DOCK, OutwardRegionName.ARK_OF_THE_EXILED_LOWER)
    SCARLET_SANCTUARY_FRONT_DOOR_EXIT = entrance("Scarlet Sanctuary - Front Door - Exit", OutwardRegionName.SCARLET_SANCTUARY, OutwardRegionName.CALDERA)
    STEAM_BATH_TUNNELS_FRONT_DOOR_EXIT = entrance("Steam Bath Tunnels - Front Door - Exit", OutwardRegionName.STEAM_BATH_TUNNELS, OutwardRegionName.CALDERA)
    SULPHURIC_CAVERNS_FRONT_DOOR_EXIT = entrance("Sulphuric Caverns - Front Door - Exit", OutwardRegionName.SULPHURIC_CAVERNS, OutwardRegionName.CALDERA)
    GROTTO_OF_CHALCEDONY_FRONT_DOOR_EXIT = entrance("The Grotto of Chalcedony - Front Door - Exit", OutwardRegionName.GROTTO_OF_CHALCEDONY, OutwardRegionName.CALDERA)
    VAULT_OF_STONE_FROTN_DOOR_EXIT = entrance("The Vault of Stone - Front Door - Exit", OutwardRegionName.VAULT_OF_STONE, OutwardRegionName.CALDERA)
    CALYGREY_COLOSSEUM_FRONT_DOOR_EXIT = entrance("Calygrey Colosseum - Front Door - Exit", OutwardRegionName.CALYGREY_COLOSSEUM, OutwardRegionName.CALDERA)
    GIANTS_SAUNA_FRONT_DOOR_EXIT = entrance("Giant's Sauna - Front Door - Exit", OutwardRegionName.GIANTS_SAUNA, OutwardRegionName.CALDERA)
    IMMACULATES_CAMP_CALDERA_FRONT_DOOR_EXIT = entrance("Immaculate's Camp - Caldera - Front Door - Exit", OutwardRegionName.IMMACULATES_CAMP_CALDERA, OutwardRegionName.CALDERA)
    OILY_CAVERN_FRONT_DOOR_EXIT = entrance("Oily Cavern - Front Door - Exit", OutwardRegionName.OILY_CAVERN, OutwardRegionName.CALDERA)
    RITUALISTS_HUT_FRONT_DOOR_EXIT = entrance("Ritualist's Hut - Front Door - Exit", OutwardRegionName.RITUALISTS_HUT, OutwardRegionName.CALDERA)
    SILKWORMS_REFUGE_FRONT_DOOR_EXIT = entrance("Silkworm's Refuge - Front Door - Exit", OutwardRegionName.SILKWORMS_REFUGE, OutwardRegionName.CALDERA)
    RIVER_OF_RED_FRONT_DOOR_EXIT = entrance("The River of Red - Front Door - Exit", OutwardRegionName.RIVER_OF_RED, OutwardRegionName.CALDERA)
    TOWER_OF_REGRETS_FRONT_DOOR_EXIT = entrance("The Tower of Regrets - Front Door - Exit", OutwardRegionName.TOWER_OF_REGRETS, OutwardRegionName.CALDERA)
    UNDERSIDE_LOADING_DOCK_FRONT_DOOR_EXIT = entrance("Underside Loading Dock - Front Door - Exit", OutwardRegionName.UNDERSIDE_LOADING_DOCK, OutwardRegionName.CALDERA)

    # other

    LEYLINE_ANY_CONFLUX_CHAMBERS_DOOR_ENTER = entrance("Leyline - Conflux Chambers - Enter", OutwardRegionName.CONFLUX_CHAMBERS, OutwardRegionName.LEYLINE_ANY)
    LEYLINE_ANY_HARMATTAN_DOOR_ENTER = entrance("Leyline - Sorobor Academy - Enter", OutwardRegionName.HARMATTAN, OutwardRegionName.LEYLINE_ANY)
