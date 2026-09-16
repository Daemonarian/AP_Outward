from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Location
from Utils import is_macos
from worlds.generic.Rules import add_item_rule, add_rule

from .common import OUTWARD
from .templates import OutwardGameObjectNamespace, OutwardGameObjectTemplate
from .items import OutwardItemName
from .factions import OutwardFaction, outward_factions
from .regions import OutwardRegionName

if TYPE_CHECKING:
    from collections.abc import Iterable

    from BaseClasses import Region
    from worlds.generic.Rules import CollectionRule, ItemRule

    from . import OutwardWorld
    from .templates import OutwardObjectTemplate

# classes for generating items in the multi-world

class OutwardLocation(Location):
    game = OUTWARD

    def __init__(self, player: int, name: str = '', address: int | None = None, parent: Region | None = None):
        super().__init__(player, name, address, parent)

    @property
    def template(self) -> OutwardObjectTemplate:
        raise NotImplementedError()
    
    @property
    def faction(self) -> OutwardFaction:
        raise NotImplementedError()

    def check_missable(self, world: OutwardWorld) -> bool:
        return False

    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        add_rule(self, rule, combine)

    def add_item_rule(self, rule: ItemRule, combine: str = "and") -> None:
        add_item_rule(self, rule, combine)

class OutwardGameLocation(OutwardLocation):
    _template: OutwardGameLocationTemplate

    def __init__(self, name: str, player: int):
        self._template = OutwardGameLocationTemplate.get_template(name)
        super().__init__(player, self._template.name, self._template.archipelago_id)

    @property
    def template(self) -> OutwardGameLocationTemplate:
        return self._template

    @property
    def missable(self) -> bool:
        return self.template.missable

    @property
    def faction(self) -> OutwardFaction:
        return self.template.faction

    def check_accessible(self, world: OutwardWorld) -> bool:
        return world.get_allowed_factions() & self.faction != 0

    def check_missable(self, world: OutwardWorld) -> bool:
        if self.missable:
            return True
        return world.get_allowed_factions() & ~self.faction != 0

    def add_to_world(self, world: OutwardWorld, *, skip_faction_check: bool = False) -> None:
        if skip_faction_check or world.get_allowed_factions() & self.template.faction != 0:
            parent = world.get_region(self.template.region)
            self.parent_region = parent
            parent.locations.append(self)
        
# classes for defining the basic properties of game locations

class OutwardGameLocationTemplate(OutwardGameObjectTemplate):
    _region: str
    _missable: bool
    _faction: OutwardFaction
    _vanilla_item_name: str | None
    def __init__(self, name: str, region: str, *, missable: bool = False, faction: OutwardFaction = OutwardFaction.AllFactions, vanilla_item_name: str | None = None, archipelago_id: int = -1):
        super().__init__(name, archipelago_id)
        self._region = region
        self._missable = missable
        self._faction = faction
        self._vanilla_item_name = vanilla_item_name
    @property
    def region(self) -> str:
        return self._region
    @property
    def missable(self) -> bool:
        return self._missable
    @property
    def faction(self) -> OutwardFaction:
        return self._faction
    @property
    def vanilla_item_name(self) -> str | None:
        return self._vanilla_item_name

# define the names of Outward locations

location = OutwardGameLocationTemplate.register_template
class OutwardLocationName(OutwardGameObjectNamespace):
    template = OutwardGameLocationTemplate

    # quest completion checks

    QUEST_MAIN_01 = location("Main Quest 1 - Castaways", OutwardRegionName.CIERZO)
    QUEST_MAIN_02 = location("Main Quest 2 - Call to Adventure", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_03 = location("Main Quest 3 - Join Faction Quest", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_04 = location("Main Quest 4 - Faction Quest 1", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_05 = location("Main Quest 5 - Faction Quest 2", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_06 = location("Main Quest 6 - Faction Quest 3", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_07 = location("Main Quest 7 - Faction Quest 4", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_08 = location("Main Quest 8 - A Fallen City", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_09 = location("Main Quest 9 - Up The Ladder", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_10 = location("Main Quest 10 - Stealing Fire", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_11 = location("Main Quest 11 - Liberate the Sun", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.QUEST_LICENSE)
    QUEST_MAIN_12 = location("Main Quest 12 - Vengeful Ouroboros", OutwardRegionName.NEW_SIROCCO)
    
    QUEST_PARALLEL_BLOOD_UNDER_THE_SUN = location("Parallel Quest - Blood Under the Sun", OutwardRegionName.LEVANT)
    QUEST_PARALLEL_PURIFIER = location("Parallel Quest - Purifier", OutwardRegionName.BERG)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_1 = location("Parallel Quest - Rust and Vengeance - Rust Lich Armor", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.RUST_LICH_ARMOR)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_2 = location("Parallel Quest - Rust and Vengeance - Rust Lich Boots", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.RUST_LICH_BOOTS)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_3 = location("Parallel Quest - Rust and Vengeance - Rust Lich Helmet", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.RUST_LICH_HELMET)
    QUEST_PARALLEL_VENDAVEL_QUEST = location("Parallel Quest - Vendavel Quest", OutwardRegionName.CIERZO)

    QUEST_MINOR_ACQUIRE_MANA = location("Minor Quest - Acquire Mana", OutwardRegionName.LEYLINE_ANY)
    QUEST_MINOR_ALCHEMY_COLD_STONE = location("Minor Quest - Alchemy: Cold Stone", OutwardRegionName.BERG)
    QUEST_MINOR_ALCHEMY_CRYSTAL_POWDER = location("Minor Quest - Alchemy: Crystal Powder", OutwardRegionName.CIERZO)
    QUEST_MINOR_ARCANE_MACHINE = location("Minor Quest - Arcane Machine", OutwardRegionName.ELECTRIC_LAB)
    QUEST_MINOR_BARREL_MAN = location("Minor Quest - Barrel Man", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.MYSTERIOUS_CHAKRAM)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_1 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Spear", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.GOLD_LICH_SPEAR)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_2 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Mask", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.GOLD_LICH_MASK)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_3 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Boots", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.GOLD_LICH_BOOTS)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_4 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Armor", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.GOLD_LICH_ARMOR)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_1 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Staff", OutwardRegionName.DARK_ZIGGURAT, vanilla_item_name=OutwardItemName.JADE_LICH_STAFF)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_2 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Boots", OutwardRegionName.DARK_ZIGGURAT, vanilla_item_name=OutwardItemName.JADE_LICH_BOOTS)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_3 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Mask", OutwardRegionName.DARK_ZIGGURAT, vanilla_item_name=OutwardItemName.JADE_LICH_MASK)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_4 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Robes", OutwardRegionName.DARK_ZIGGURAT, vanilla_item_name=OutwardItemName.JADE_LICH_ROBES)
    QUEST_MINOR_BLOODY_BUSINESS = location("Minor Quest - Bloody Business", OutwardRegionName.BLOOD_MAGE_HIDEOUT, vanilla_item_name=OutwardItemName.CURSE_HEX)
    QUEST_MINOR_CRAFT_ANTIQUE_PLATE_GARB_ARMOR = location("Minor Quest - Craft: Antique Plate Garb Armor", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_CRAFT_BLUE_SAND_ARMOR = location("Minor Quest - Craft: Blue Sand Armor", OutwardRegionName.CIERZO)
    QUEST_MINOR_CRAFT_COPAL_AND_PETRIFIED_ARMOR = location("Minor Quest - Craft: Copal & Petrified Armor", OutwardRegionName.BERG)
    QUEST_MINOR_CRAFT_PALLADIUM_ARMOR = location("Minor Quest - Craft: Palladium Armor", OutwardRegionName.MONSOON)
    QUEST_MINOR_CRAFT_TSAR_AND_TENEBROUS_ARMOR = location("Minor Quest - Craft: Tsar & Tenebrous Armor", OutwardRegionName.LEVANT)
    QUEST_MINOR_HELENS_FUNGUS = location("Minor Quest - Helen's Fungus", OutwardRegionName.CIERZO)
    QUEST_MINOR_LEDGER_TO_BERG = location("Minor Quest - Ledger to Berg", OutwardRegionName.BERG)
    QUEST_MINOR_LEDGER_TO_CIERZO = location("Minor Quest - Ledger to Cierzo", OutwardRegionName.CIERZO)
    QUEST_MINOR_LEDGER_TO_LEVANT = location("Minor Quest - Ledger to Levant", OutwardRegionName.LEVANT)
    QUEST_MINOR_LEDGER_TO_MONSOON = location("Minor Quest - Ledger to Monsoon", OutwardRegionName.MONSOON)
    QUEST_MINOR_LOST_MERCHANT = location("Minor Quest - Lost Merchant", OutwardRegionName.LEVANT_SLUMS)
    QUEST_MINOR_MYRIAD_OF_BONES = location("Minor Quest - A Myriad of Bones", OutwardRegionName.CORRUPTED_TOMBS)
    QUEST_MINOR_NEED_ANGEL_FOOD_CAKE = location("Minor Quest - Need: Angel Food Cake", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_NEED_BEAST_GOLEM_SCRAPS = location("Minor Quest - Need: Beast Golem Scraps", OutwardRegionName.BERG)
    QUEST_MINOR_NEED_CIERZO_CEVICHE = location("Minor Quest - Need: Cierzo Ceviche", OutwardRegionName.CIERZO)
    QUEST_MINOR_NEED_FIRE_ELEMENTAL_PARTICLES = location("Minor Quest - Need: Fire Elemental Particles", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_NEED_MANAHEART_BASS = location("Minor Quest - Need: Manaheart Bass", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_NEED_MANTICORE_TAIL = location("Minor Quest - Need: Manticore Tail", OutwardRegionName.BERG)
    QUEST_MINOR_NEED_SHARK_CARTILAGE = location("Minor Quest - Need: Shark Cartilage", OutwardRegionName.LEVANT)
    QUEST_MINOR_NEED_SHIELD_GOLEM_SCRAP = location("Minor Quest - Need: Shield Golem Scrap", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_NEED_TOURMALINE = location("Minor Quest - Need: Tourmaline", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_PURIFY_THE_WATER = location("Minor Quest - Purify the Water", OutwardRegionName.CIERZO)
    QUEST_MINOR_RED_IDOL = location("Minor Quest - Red Idol", OutwardRegionName.FACE_OF_THE_ANCIENTS)
    QUEST_MINOR_SCHOLARS_RANSOM = location("Minor Quest - A Scholar's Ransom", OutwardRegionName.HARMATTAN)
    QUEST_MINOR_SILVER_FOR_THE_SLUMS = location("Minor Quest - Silver for the Slums", OutwardRegionName.LEVANT_SLUMS)
    QUEST_MINOR_SKULLS_FOR_CREMEUH = location("Minor Quest - Skulls for Cremeuh", OutwardRegionName.NECROPOLIS, vanilla_item_name=OutwardItemName.ORNATE_BONE_SHIELD)
    QUEST_MINOR_STRANGE_APPARITIONS = location("Minor Quest - Strange Apparitions", OutwardRegionName.HALLOWED_MARSH, vanilla_item_name=OutwardItemName.LIGHT_MENDERS_BACKPACK)
    QUEST_MINOR_TREASURE_HUNT = location("Minor Quest - Treasure Hunt", OutwardRegionName.PIRATES_HIDEOUT)
    QUEST_MINOR_WILLIAM_OF_THE_WISP = location("Minor Quest - William of the Wisp", OutwardRegionName.ENMERKAR_FOREST, vanilla_item_name=OutwardItemName.THE_WILL_O_WISP)

    # comissions

    COMMISSION_ANTIQUE_PLATE_BOOTS = location("Commission - Antique Plate Boots", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.ANTIQUE_PLATE_BOOTS)
    COMMISSION_ANTIQUE_PLATE_GARB = location("Commission - Antique Plate Garb", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.ANTIQUE_PLATE_GARB)
    COMMISSION_ANTIQUE_PLATE_SALLET = location("Commission - Antique Plate Sallet", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.ANTIQUE_PLATE_SALLET)

    COMMISSION_BLUE_SAND_ARMOR = location("Commission - Blue Sand Armor", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.BLUE_SAND_ARMOR)
    COMMISSION_BLUE_SAND_BOOTS = location("Commission - Blue Sand Boots", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.BLUE_SAND_BOOTS)
    COMMISSION_BLUE_SAND_HELM = location("Commission - Blue Sand Helm", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.BLUE_SAND_HELM)

    COMMISSION_COPAL_ARMOR = location("Commission - Copal Armor", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.COPAL_ARMOR)
    COMMISSION_COPAL_BOOTS = location("Commission - Copal Boots", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.COPAL_BOOTS)
    COMMISSION_COPAL_HELM = location("Commission - Copal Helm", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.COPAL_HELM)

    COMMISSION_PALLADIUM_ARMOR = location("Commission - Palladium Armor", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.PALLADIUM_ARMOR)
    COMMISSION_PALLADIUM_BOOTS = location("Commission - Palladium Boots", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.PALLADIUM_BOOTS)
    COMMISSION_PALLADIUM_HELM = location("Commission - Palladium Helm", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.PALLADIUM_HELM)

    COMMISSION_PETRIFIED_WOOD_ARMOR = location("Commission - Petrified Wood Armor", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.PETRIFIED_WOOD_ARMOR)
    COMMISSION_PETRIFIED_WOOD_BOOTS = location("Commission - Petrified Wood Boots", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.PETRIFIED_WOOD_BOOTS)
    COMMISSION_PETRIFIED_WOOD_HELM = location("Commission - Petrified Wood Helm", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.PETRIFIED_WOOD_HELM)

    COMMISSION_TENEBROUS_ARMOR = location("Commission - Tenebrous Armor", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TENEBROUS_ARMOR)
    COMMISSION_TENEBROUS_BOOTS = location("Commission - Tenebrous Boots", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TENEBROUS_BOOTS)
    COMMISSION_TENEBROUS_HELM = location("Commission - Tenebrous Helm", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TENEBROUS_HELM)

    COMMISSION_TSAR_ARMOR = location("Commission - Tsar Armor", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TSAR_ARMOR)
    COMMISSION_TSAR_BOOTS = location("Commission - Tsar Boots", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TSAR_BOOTS)
    COMMISSION_TSAR_HELM = location("Commission - Tsar Helm", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.TSAR_HELM)

    # unique items

    SPAWN_ANGLER_SHIELD = location("Item Upgrade - Caldera - Slumbering Shield", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.ANGLER_SHIELD)
    SPAWN_BRAND = location("Item Upgrade - Strange Rusted Sword", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.BRAND)
    SPAWN_BRASS_WOLF_BACKPACK = location("Spawn - Brass-Wolf Backpack", OutwardRegionName.SAND_ROSE_CAVE, vanilla_item_name=OutwardItemName.BRASS_WOLF_BACKPACK)
    SPAWN_CEREMONIAL_BOW = location("Spawn - Ceremonial Bow", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.CEREMONIAL_BOW)
    SPAWN_COMPASSWOOD_STAFF = location("The Walled Garden - Guardian of the Compass - Compasswood Staff", OutwardRegionName.ABRASSAR, vanilla_item_name=OutwardItemName.COMPASSWOOD_STAFF)
    SPAWN_CRACKED_RED_MOON = location("Scarlet Sanctuary - Crimson Avatar - Cracked Red Moon", OutwardRegionName.SCARLET_SANCTUARY, vanilla_item_name=OutwardItemName.CRACKED_RED_MOON)
    SPAWN_DEPOWERED_BLUDGEON = location("Spawn - De-powered Bludgeon", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.DEPOWERED_BLUDGEON)
    SPAWN_DISTORTED_EXPERIMENT = location("Item Upgrade - Caldera - Experimental Chakram", OutwardRegionName.ARK_OF_THE_EXILED_UPPER, vanilla_item_name=OutwardItemName.DISTORTED_EXPERIMENT)
    SPAWN_DREAMER_HALBERD = location("Friendly Immaculate Gift - Dreamer Halberd", OutwardRegionName.IMMACULATE_CAMP_ANY, vanilla_item_name=OutwardItemName.DREAMER_HALBERD)
    SPAWN_DUTY = location("Item Upgrade - Silkworm's Refuge - Ruined Halberd", OutwardRegionName.SILKWORMS_REFUGE, vanilla_item_name=OutwardItemName.DUTY)
    SPAWN_EXPERIMENTAL_CHAKRAM = location("Spawn - Experimental Chakram", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.EXPERIMENTAL_CHAKRAM)
    SPAWN_FABULOUS_PALLADIUM_SHIELD = location("Spawn - Fabulous Palladium Shield", OutwardRegionName.IMMACULATES_CAMP_CALDERA, vanilla_item_name=OutwardItemName.FABULOUS_PALLADIUM_SHIELD)
    SPAWN_FOSSILIZED_GREATAXE = location("Steam Bath Tunnels - Calygrey Hero - Fossilized Greataxe", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.FOSSILIZED_GREATAXE)
    SPAWN_GEPS_BLADE = location("Item Upgrade - Mysterious Blade/Long Blade", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.GEPS_LONGBLADE)
    SPAWN_GHOST_PARALLEL = location("Item Upgrade - De-Powered Bludgeon", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.GHOST_PARALLEL)
    SPAWN_GILDED_SHIVER_OF_TRAMONTANE = location("Item Upgrade - Scarred Dagger", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.GILDED_SHIVER_OF_TRAMONTANE)
    SPAWN_GRIND = location("Item Upgrade - Oily Cavern - Fossilized Greataxe", OutwardRegionName.OILY_CAVERN, vanilla_item_name=OutwardItemName.GRIND)
    SPAWN_KRYPTEIA_TOMB_KEY = location("Face of the Ancients - The First Cannibal - Krypteia Tomb Key", OutwardRegionName.FACE_OF_THE_ANCIENTS, vanilla_item_name=OutwardItemName.KRYPTEIA_TOMB_KEY)
    SPAWN_LIGHT_MENDERS_LEXICON = location("Spawn - Light Mender's Lexicon", OutwardRegionName.VOLTAIC_HATCHERY, vanilla_item_name=OutwardItemName.LIGHT_MENDERS_LEXICON)
    SPAWN_MEFINOS_TRADE_BACKPACK = location("Spawn - Mefino's Trade Backpack", OutwardRegionName.MONTCALM_CLAN_FORT, vanilla_item_name=OutwardItemName.MEFINOS_TRADE_BACKPACK)
    SPAWN_MERTONS_FIREPOKER = location("Spawn - Merton's Firepoker", OutwardRegionName.TREE_HUSK, vanilla_item_name=OutwardItemName.MERTONS_FIREPOKER)
    SPAWN_MERTONS_RIBCAGE = location("Spawn - Merton's Ribcage", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.MERTONS_RIBCAGE)
    SPAWN_MERTONS_SHINBONES = location("Spawn - Merton's Shinbones", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.MERTONS_SHINBONES)
    SPAWN_MERTONS_SKULL = location("Spawn - Merton's Skull", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.MERTONS_SKULL)
    SPAWN_MURMURE = location("Item Upgrade - Ceremonial Bow", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.MURMURE)
    SPAWN_MYSTERIOUS_LONG_BLADE = location("Spawn - Mysterious Long Blade", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.MYSTERIOUS_LONG_BLADE)
    SPAWN_PEARLESCENT_MAIL = location("Ruins of Old Levant - Luke the Pearlescent - Pearlescent Mail", OutwardRegionName.RUINS_OF_OLD_LEVANT, vanilla_item_name=OutwardItemName.PEARLESCENT_MAIL)
    SPAWN_PILLAR_GREATHAMMER = location("Spawn - Pillar Greathammer", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.PILLAR_GREATHAMMER)
    SPAWN_PORCELAIN_FISTS = location("Crumbling Loading Docks - Giant Horror - Porcelain Fists", OutwardRegionName.CRUMBLING_LOADING_DOCKS, vanilla_item_name=OutwardItemName.PORCELAIN_FISTS)
    SPAWN_RED_LADYS_DAGGER = location("Face of the Ancients - Red Lady's Altar - Krypteia Tomb Key", OutwardRegionName.FACE_OF_THE_ANCIENTS, vanilla_item_name=OutwardItemName.RED_LADYS_DAGGER)
    SPAWN_REVENANT_MOON = location("Item Upgrade - Cracked Red Moon", OutwardRegionName.UNPLACED, vanilla_item_name=OutwardItemName.REVENANT_MOON)
    SPAWN_ROTWOOD_STAFF = location("Item Upgrade - Necroplis - Compasswood Staff", OutwardRegionName.NECROPOLIS, vanilla_item_name=OutwardItemName.ROTWOOD_STAFF)
    SPAWN_RUINED_HALBERD = location("Spawn - Ruined Halberd", OutwardRegionName.OIL_REFINERY, vanilla_item_name=OutwardItemName.RUINED_HALBERD)
    SPAWN_RUSTED_SPEAR = location("Spawn - Rusted Spear", OutwardRegionName.UNDERSIDE_LOADING_DOCK, vanilla_item_name=OutwardItemName.RUSTED_SPEAR)
    SPAWN_SANDROSE = location("Item Upgrade - Eldest Brother - Warm Axe", OutwardRegionName.ELDEST_BROTHER, vanilla_item_name=OutwardItemName.SANDROSE)
    SPAWN_SCARLET_BOOTS = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Boots", OutwardRegionName.SCARLET_SANCTUARY, vanilla_item_name=OutwardItemName.SCARLET_BOOTS)
    SPAWN_SCARLET_GEM = location("Spawn - Scarlet Gem", OutwardRegionName.SCARLET_SANCTUARY, vanilla_item_name=OutwardItemName.SCARLET_GEM)
    SPAWN_SCARLET_LICHS_IDOL = location("Spawn - Scarlet Lich's Idol", OutwardRegionName.FACE_OF_THE_ANCIENTS, vanilla_item_name=OutwardItemName.SCARLET_LICHS_IDOL)
    SPAWN_SCARLET_MASK = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Mask", OutwardRegionName.SCARLET_SANCTUARY, vanilla_item_name=OutwardItemName.SCARLET_MASK)
    SPAWN_SCARLET_ROBES = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Robes", OutwardRegionName.SCARLET_SANCTUARY, vanilla_item_name=OutwardItemName.SCARLET_ROBES)
    SPAWN_SCARRED_DAGGER = location("Spawn - Scarred Dagger", OutwardRegionName.GROTTO_OF_CHALCEDONY, vanilla_item_name=OutwardItemName.SCARRED_DAGGER)
    SPAWN_SCEPTER_OF_THE_CRUEL_PRIEST = location("Item Upgrade - Giant's Sauna - Sealed Mace", OutwardRegionName.GIANTS_SAUNA, vanilla_item_name=OutwardItemName.SCEPTER_OF_THE_CRUEL_PRIEST)
    SPAWN_SEALED_MACE = location("Old Sirocco - Smelly Sealed Box interaction with forge", OutwardRegionName.OLD_SIROCCO, vanilla_item_name=OutwardItemName.SEALED_MACE)
    SPAWN_SHRIEK = location("Item Upgrade - Sulphuric Caverns - Rusted Spear", OutwardRegionName.SULPHURIC_CAVERNS, vanilla_item_name=OutwardItemName.SHRIEK)
    SPAWN_SKYCROWN_MACE = location("Face of the Ancients - The First Cannibal - Skycrown Mace", OutwardRegionName.FACE_OF_THE_ANCIENTS, vanilla_item_name=OutwardItemName.SKYCROWN_MACE)
    SPAWN_SLUMBERING_SHIELD = location("Spawn - Slumbering Shield", OutwardRegionName.VAULT_OF_STONE, vanilla_item_name=OutwardItemName.SLUMBERING_SHIELD)
    SPAWN_SMELLY_SEALED_BOX = location("Spawn - Smelly Sealed Box", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.SMELLY_SEALED_BOX)
    SPAWN_STARCHILD_CLAYMORE = location("Enmerkar Forest - Royal Manticore - Starchild Claymore", OutwardRegionName.ENMERKAR_FOREST, vanilla_item_name=OutwardItemName.STARCHILD_CLAYMORE)
    SPAWN_STRANGE_RUSTED_SWORD = location("Spawn - Strange Rusted Sword", OutwardRegionName.CHERSONESE, vanilla_item_name=OutwardItemName.STRANGE_RUSTED_SWORD)
    SPAWN_SUNFALL_AXE = location("Spawn - Sunfall Axe", OutwardRegionName.STONE_TITAN_CAVES, vanilla_item_name=OutwardItemName.SUNFALL_AXE)
    SPAWN_THRICE_WROUGHT_HALBERD = location("Spawn - Thrice-Wrought Halberd", OutwardRegionName.CABAL_OF_WIND_TEMPLE, vanilla_item_name=OutwardItemName.THRICE_WROUGHT_HALBERD)
    SPAWN_TOKEBAKICIT = location("Item Upgrade - Unusual Knuckles", OutwardRegionName.STEAM_BATH_TUNNELS, vanilla_item_name=OutwardItemName.TOKEBAKICIT)
    SPAWN_TSAR_FISTS = location("Spawn - Tsar Fists", OutwardRegionName.FORGOTTEN_RESEARCH_LABORATORY, vanilla_item_name=OutwardItemName.TSAR_FISTS)
    SPAWN_UNUSUAL_KNUCKLES = location("Spawn - Unusual Knuckles", OutwardRegionName.STEAM_BATH_TUNNELS, vanilla_item_name=OutwardItemName.UNUSUAL_KNUCKLES)
    SPAWN_WARM_AXE = location("Spawn - Warm Axe", OutwardRegionName.MYRMITAURS_HAVEN, vanilla_item_name=OutwardItemName.WARM_AXE)
    SPAWN_WERLIG_SPEAR = location("Spawn - Werlig Spear", OutwardRegionName.ELECTRIC_LAB_EXTERIOR, vanilla_item_name=OutwardItemName.WERLIG_SPEAR)
    SPAWN_WORLDEDGE_GREATAXE = location("Enmerkar Forest - Tyrant of the Hive - Worldedge Greataxe", OutwardRegionName.FOREST_HIVES_EXTERIOR, vanilla_item_name=OutwardItemName.WORLDEDGE_GREATAXE)
    SPAWN_ZHORNS_DEMON_SHIELD = location("Spawn - Zhorn's Demon Shield", OutwardRegionName.JADE_QUARRY, vanilla_item_name=OutwardItemName.ZHORNS_DEMON_SHIELD)
    SPAWN_ZHORNS_GLOWSTONE_DAGGER = location("Spawn - Zhorn's Glowstone Dagger", OutwardRegionName.HALLOWED_MARSH, vanilla_item_name=OutwardItemName.ZHORNS_GLOWSTONE_DAGGER)
    SPAWN_ZHORNS_HUNTING_BACKPACK = location("Spawn - Zhorn's Hunting Backpack", OutwardRegionName.ROYAL_MANTICORES_LAIR, vanilla_item_name=OutwardItemName.ZHORNS_HUNTING_BACKPACK)
    SPAWN_MYRMITAUR_HAVEN_GATE_KEY = location("Myrmitaur Haven - Matriarch Myrmitaur - Myrmitaur Haven Gate Key", OutwardRegionName.MYRMITAURS_HAVEN, vanilla_item_name=OutwardItemName.MYRMITAUR_HAVEN_GATE_KEY)

    # individual skills learned

    BURAC_FREE_SKILL = location("Burac - Free Skill", OutwardRegionName.CIERZO, missable=True)
    WATCHER_FREE_SKILL = location("Watcher - Free Skill", OutwardRegionName.LEYLINE_ANY)
    TRAIN_VENDAVEL_PRISONER = location("Train - Vendavel Prisoner", OutwardRegionName.VENDAVEL_FORTRESS)
    TRAIN_FIRST_WATCHER = location("Train - First Watcher", OutwardRegionName.CONFLUX_CHAMBERS, vanilla_item_name=OutwardItemName.FLAMETHROWER)
    TRAIN_TALERON = location("Train - Taleron", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.MACE_INFUSION)
    TRAIN_MARKUS = location("Train - Markus", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.PUNCTURE)
    TRAIN_SAGARD_BATTLEBORN = location("Train - Sagard Battleborn", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.EXECUTION)
    TRAIN_SOERAN = location("Train - Soeran", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.JUGGERNAUT)
    TRAIN_WANDERING_MERCENARY = location("Train - Wandering Mercenary", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.MOON_SWIPE)
    TRAIN_KING_SIMEON = location("Train - King Simeon", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.SIMEONS_GAMBIT)
    TRAIN_BURAC = location("Train - Burac", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.POMMEL_COUNTER)
    TRAIN_ODA = location("Train - Oda", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.TALUS_CLEAVER)
    TRAIN_SAMANTHA_TURNBULL = location("Train - Samantha Turnbull", OutwardRegionName.NEW_SIROCCO, faction=OutwardFaction.SoroborAcademy, vanilla_item_name=OutwardItemName.INFUSE_MANA)
    TRAIN_ANTHONY_BERTHELOT = location("Train - Anthony Berthelot", OutwardRegionName.NEW_SIROCCO, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.INFUSE_BLOOD)
    TRAIN_SMOOTH = location("Train - Smooth", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.WARM)
    TRAIN_SECOND_WATCHER = location("Train - Second Watcher", OutwardRegionName.CONFLUX_CHAMBERS, vanilla_item_name=OutwardItemName.COOL)
    TRAIN_CYRIL_TURNBULL = location("Train - Cyril Turnbull", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.MIST)
    TRAIN_MOFAT = location("Train - Mofat", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.BLESSED)
    TRAIN_NINTH_WATCHER = location("Train - The 9th Watcher", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.SCORCH_HEX)
    TRAIN_SKELETON = location("Train - Skeleton", OutwardRegionName.WENDIGO_LAIR, vanilla_item_name=OutwardItemName.CHILL_HEX)
    TRAIN_RAUL_SALABERRY = location("Train - Headmaster Raul Salaberry", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.DOOM_HEX)
    TRAIN_ROBYN_GARNET = location("Train - Robyn Garnet, Alchemist", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.HAUNT_HEX)
    TRAIN_MAVITH = location("Train - Mavith", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.PRISMATIC_FLURRY)
    TRAIN_PAUL = location("Train - Paul, Disciple", OutwardRegionName.NEW_SIROCCO, faction=OutwardFaction.HolyMission, vanilla_item_name=OutwardItemName.ELATTS_INTERVENTION)
    TRAIN_YAN = location("Train - Yan, Levantine Alchemist", OutwardRegionName.NEW_SIROCCO, faction=OutwardFaction.HeroicKingdom, vanilla_item_name=OutwardItemName.KIROUACS_BREAKTHROUGH)
    REPAIR_HORSE_STATUE = location("Repair Horse Statue", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.SEVERED_OBSIDIAN)
    SKILL_BLADE_PUPPY = location("Drop - Plague Doctor - Blade Puppy", OutwardRegionName.DARK_ZIGGURAT, vanilla_item_name=OutwardItemName.BLADE_PUPPY)
    SKILL_GOLDEN_WATCHER = location("Drop - Light Mender - Golden Watcher", OutwardRegionName.SPIRE_OF_LIGHT, vanilla_item_name=OutwardItemName.GOLDEN_WATCHER)

    # skill trainer

    SKILL_TRAINER_ADALBERT_CALL_TO_ELEMENTS = location("Skill Trainer - Adalbert the Hermit - Call to Elements", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.CALL_TO_ELEMENTS)
    SKILL_TRAINER_ADALBERT_MANA_PUSH = location("Skill Trainer - Adalbert the Hermit - Mana Push", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.MANA_PUSH)
    SKILL_TRAINER_ADALBERT_REVEAL_SOUL = location("Skill Trainer - Adalbert the Hermit - Reveal Soul", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.REVEAL_SOUL)
    SKILL_TRAINER_ADALBERT_WEATHER_TOLERANCE = location("Skill Trainer - Adalbert the Hermit - Weather Tolerance", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.WEATHER_TOLERANCE)
    SKILL_TRAINER_ADALBERT_SHAMANIC_RESONANCE = location("Skill Trainer - Adalbert the Hermit - Shamanic Resonance", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.SHAMANIC_RESONANCE)
    SKILL_TRAINER_ADALBERT_SIGIL_OF_WIND = location("Skill Trainer - Adalbert the Hermit - Sigil of Wind", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.SIGIL_OF_WIND)
    SKILL_TRAINER_ADALBERT_INFUSE_WIND = location("Skill Trainer - Adalbert the Hermit - Infuse Wind", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.INFUSE_WIND)
    SKILL_TRAINER_ADALBERT_CONJURE = location("Skill Trainer - Adalbert the Hermit - Conjure", OutwardRegionName.HERMITS_HOUSE, vanilla_item_name=OutwardItemName.CONJURE)

    SKILL_TRAINER_ALEMMON_CHAKRAM_ARC = location("Skill Trainer - Alemmon - Chakram Arc", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.CHAKRAM_ARC)
    SKILL_TRAINER_ALEMMON_CHAKRAM_PIERCE = location("Skill Trainer - Alemmon - Chakram Pierce", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.CHAKRAM_PIERCE)
    SKILL_TRAINER_ALEMMON_MANA_WARD = location("Skill Trainer - Alemmon - Mana Ward", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.MANA_WARD)
    SKILL_TRAINER_ALEMMON_SIGIL_OF_FIRE = location("Skill Trainer - Alemmon - Sigil of Fire", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.SIGIL_OF_FIRE)
    SKILL_TRAINER_ALEMMON_LEYLINE_CONNECTION = location("Skill Trainer - Alemmon - Leyline Connection", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.LEYLINE_CONNECTION)
    SKILL_TRAINER_ALEMMON_CHAKRAM_DANCE = location("Skill Trainer - Alemmon - Chakram Dance", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.CHAKRAM_DANCE)
    SKILL_TRAINER_ALEMMON_SIGIL_OF_ICE = location("Skill Trainer - Alemmon - Sigil of Ice", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.SIGIL_OF_ICE)
    SKILL_TRAINER_ALEMMON_FIRE_AFFINITY = location("Skill Trainer - Alemmon - Fire Affinity", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.FIRE_AFFINITY)

    SKILL_TRAINER_BEA_WARRIORS_VEIN = location("Skill Trainer - Bea Battleborn - Warrior's Vein", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.WARRIORS_VEIN)
    SKILL_TRAINER_BEA_DISPERSION = location("Skill Trainer - Bea Battleborn - Dispersion", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.DISPERSION)
    SKILL_TRAINER_BEA_MOMENT_OF_TRUTH = location("Skill Trainer - Bea Battleborn - Moment of Truth", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.MOMENT_OF_TRUTH)
    SKILL_TRAINER_BEA_TECHNIQUE = location("Skill Trainer - Bea Battleborn - The Technique", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.TECHNIQUE)
    SKILL_TRAINER_BEA_SCALP_COLLECTOR = location("Skill Trainer - Bea Battleborn - Scalp Collector", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.SCALP_COLLECTOR)
    SKILL_TRAINER_BEA_CRESCENDO = location("Skill Trainer - Bea Battleborn - Crescendo", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.CRESCENDO)
    SKILL_TRAINER_BEA_VICIOUS_CYCLE = location("Skill Trainer - Bea Battleborn - Vicious Cycle", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.VICIOUS_CYCLE)
    SKILL_TRAINER_BEA_SPLITTER = location("Skill Trainer - Bea Battleborn - Splitter", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.SPLITTER)
    SKILL_TRAINER_BEA_VITAL_CRASH = location("Skill Trainer - Bea Battleborn - Vital Crash", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.VITAL_CRASH)
    SKILL_TRAINER_BEA_STRAFING_RUN = location("Skill Trainer - Bea Battleborn - Strafing Run", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.STRAFING_RUN)

    SKILL_TRAINER_ELLA_JINX = location("Skill Trainer - Ella Lockwell - Jinx", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.JINX)
    SKILL_TRAINER_ELLA_NIGHTMARES = location("Skill Trainer - Ella Lockwell - Nightmares", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.NIGHTMARES)
    SKILL_TRAINER_ELLA_TORMENT = location("Skill Trainer - Ella Lockwell - Torment", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.TORMENT)
    SKILL_TRAINER_ELLA_BLOODLUST = location("Skill Trainer - Ella Lockwell - Bloodlust", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.BLOODLUST)
    SKILL_TRAINER_ELLA_BLOOD_SIGIL = location("Skill Trainer - Ella Lockwell - Blood Sigil", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.BLOOD_SIGIL)
    SKILL_TRAINER_ELLA_RUPTURE = location("Skill Trainer - Ella Lockwell - Rupture", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.RUPTURE)
    SKILL_TRAINER_ELLA_CLEANSE = location("Skill Trainer - Ella Lockwell - Cleanse", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.CLEANSE)
    SKILL_TRAINER_ELLA_LOCKWELLS_REVELATION = location("Skill Trainer - Ella Lockwell - Lockwell's Revelation", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.LOCKWELLS_REVELATION)

    SKILL_TRAINER_ETO_FITNESS = location("Skill Trainer - Eto - Fitness", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.FITNESS)
    SKILL_TRAINER_ETO_SHIELD_CHARGE = location("Skill Trainer - Eto - Shield Charge", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.SHIELD_CHARGE)
    SKILL_TRAINER_ETO_STEADY_ARM = location("Skill Trainer - Eto - Steady Arm", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.STEADY_ARM)
    SKILL_TRAINER_ETO_SPELLBLADES_AWAKENING = location("Skill Trainer - Eto - Spellblade's Awakening", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.SPELLBLADES_AWAKENING)
    SKILL_TRAINER_ETO_INFUSE_FIRE = location("Skill Trainer - Eto - Infuse Fire", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.INFUSE_FIRE)
    SKILL_TRAINER_ETO_INFUSE_FROST = location("Skill Trainer - Eto - Infuse Frost", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.INFUSE_FROST)
    SKILL_TRAINER_ETO_GONG_STRIKE = location("Skill Trainer - Eto - Gong Strike", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.GONG_STRIKE)
    SKILL_TRAINER_ETO_ELEMENTAL_DISCHARGE = location("Skill Trainer - Eto - Elemental Discharge", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.ELEMENTAL_DISCHARGE)

    SKILL_TRAINER_FLASE_RUNE_DEZ = location("Skill Trainer - Flase, Sage Trainer - Rune: Dez", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.RUNE_DEZ)
    SKILL_TRAINER_FLASE_RUNE_EGOTH = location("Skill Trainer - Flase, Sage Trainer - Rune: Egoth", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.RUNE_EGOTH)
    SKILL_TRAINER_FLASE_RUNE_FAL = location("Skill Trainer - Flase, Sage Trainer - Rune: Fal", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.RUNE_FAL)
    SKILL_TRAINER_FLASE_RUNE_SHIM = location("Skill Trainer - Flase, Sage Trainer - Rune: Shim", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.RUNE_SHIM)
    SKILL_TRAINER_FLASE_WELL_OF_MANA = location("Skill Trainer - Flase, Sage Trainer - Well of Mana", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.WELL_OF_MANA)
    SKILL_TRAINER_FLASE_ARCANE_SYNTAX = location("Skill Trainer - Flase, Sage Trainer - Arcane Syntax", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.ARCANE_SYNTAX)
    SKILL_TRAINER_FLASE_INTERNALIZED_LEXICON = location("Skill Trainer - Flase, Sage Trainer - Internalized Lexicon", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.INTERNALIZED_LEXICON)
    SKILL_TRAINER_FLASE_RUNIC_PREFIX = location("Skill Trainer - Flase, Sage Trainer - Runic Prefix", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.RUNIC_PREFIX)

    SKILL_TRAINER_GALIRA_BRACE = location("Skill Trainer - Galira - Brace", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.BRACE)
    SKILL_TRAINER_GALIRA_FOCUS = location("Skill Trainer - Galira - Focus", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.FOCUS)
    SKILL_TRAINER_GALIRA_SLOW_METABOLISM = location("Skill Trainer - Galira - Slow Metabolism", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.SLOW_METABOLISM)
    SKILL_TRAINER_GALIRA_STEADFAST_ASCETIC = location("Skill Trainer - Galira - Steadfast Ascetic", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.STEADFAST_ASCETIC)
    SKILL_TRAINER_GALIRA_PERFECT_STRIKE = location("Skill Trainer - Galira - Perfect Strike", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.PERFECT_STRIKE)
    SKILL_TRAINER_GALIRA_MASTER_OF_MOTION = location("Skill Trainer - Galira - Master of Motion", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.MASTER_OF_MOTION)
    SKILL_TRAINER_GALIRA_FLASH_ONSLAUGHT = location("Skill Trainer - Galira - Flash Onslaught", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.FLASH_ONSLAUGHT)
    SKILL_TRAINER_GALIRA_COUNTERSTRIKE = location("Skill Trainer - Galira - Counterstrike", OutwardRegionName.MONSOON, vanilla_item_name=OutwardItemName.COUNTERSTRIKE)

    SKILL_TRAINER_JAIMON_ARMOR_TRAINING = location("Skill Trainer - Jaimon - Armor Training", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.ARMOR_TRAINING)
    SKILL_TRAINER_JAIMON_FAST_MAINTENANCE = location("Skill Trainer - Jaimon - Fast Maintenance", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.FAST_MAINTENANCE)
    SKILL_TRAINER_JAIMON_FROST_BULLET = location("Skill Trainer - Jaimon - Frost Bullet", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.FROST_BULLET)
    SKILL_TRAINER_JAIMON_SHATTER_BULLET = location("Skill Trainer - Jaimon - Shatter Bullet", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.SHATTER_BULLET)
    SKILL_TRAINER_JAIMON_SWIFT_FOOT = location("Skill Trainer - Jaimon - Swift Foot", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.SWIFT_FOOT)
    SKILL_TRAINER_JAIMON_MARATHONER = location("Skill Trainer - Jaimon - Marathoner", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.MARATHONER)
    SKILL_TRAINER_JAIMON_SHIELD_INFUSION = location("Skill Trainer - Jaimon - Shield Infusion", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.SHIELD_INFUSION)
    SKILL_TRAINER_JAIMON_BLOOD_BULLET = location("Skill Trainer - Jaimon - Blood Bullet", OutwardRegionName.LEVANT, vanilla_item_name=OutwardItemName.BLOOD_BULLET)

    SKILL_TRAINER_JUSTIN_ACROBATICS = location("Skill Trainer - Justin Garnet - Acrobatics", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.ACROBATICS)
    SKILL_TRAINER_JUSTIN_BRAINS = location("Skill Trainer - Justin Garnet - Brains", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.BRAINS)
    SKILL_TRAINER_JUSTIN_BRAWNS = location("Skill Trainer - Justin Garnet - Brawns", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.BRAWNS)
    SKILL_TRAINER_JUSTIN_CRUELTY = location("Skill Trainer - Justin Garnet - Cruelty", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.CRUELTY)
    SKILL_TRAINER_JUSTIN_PATIENCE = location("Skill Trainer - Justin Garnet - Patience", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.PATIENCE)
    SKILL_TRAINER_JUSTIN_UNSEALED = location("Skill Trainer - Justin Garnet - Unsealed", OutwardRegionName.NEW_SIROCCO, vanilla_item_name=OutwardItemName.UNSEALED)

    SKILL_TRAINER_SERGE_EFFICIENCY = location("Skill Trainer - Serge Battleborn - Efficiency", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.EFFICIENCY)
    SKILL_TRAINER_SERGE_METABOLIC_PURGE = location("Skill Trainer - Serge Battleborn - Metabolic Purge", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.METABOLIC_PURGE)
    SKILL_TRAINER_SERGE_PROBE = location("Skill Trainer - Serge Battleborn - Probe", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.PROBE)
    SKILL_TRAINER_SERGE_DAREDEVIL = location("Skill Trainer - Serge Battleborn - Daredevil", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.DAREDEVIL)
    SKILL_TRAINER_SERGE_PRIME = location("Skill Trainer - Serge Battleborn - Prime", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.PRIME)
    SKILL_TRAINER_SERGE_UNERRING_READ = location("Skill Trainer - Serge Battleborn - Unerring Read", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.UNERRING_READ)
    SKILL_TRAINER_SERGE_BLITZ = location("Skill Trainer - Serge Battleborn - Blitz", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.BLITZ)
    SKILL_TRAINER_SERGE_ANTICIPATION = location("Skill Trainer - Serge Battleborn - Anticipation", OutwardRegionName.HARMATTAN, vanilla_item_name=OutwardItemName.ANTICIPATION)

    SKILL_TRAINER_SINAI_HAUNTING_BEAT = location("Skill Trainer - Sinai, the Primal Ritualist - Haunting Beat", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.HAUNTING_BEAT)
    SKILL_TRAINER_SINAI_MIASMIC_TOLERANCE = location("Skill Trainer - Sinai, the Primal Ritualist - Miasmic Tolerance", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.MIASMIC_TOLERANCE)
    SKILL_TRAINER_SINAI_WELKIN_RING = location("Skill Trainer - Sinai, the Primal Ritualist - Welkin Ring", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.WELKIN_RING)
    SKILL_TRAINER_SINAI_SACRED_FUMES = location("Skill Trainer - Sinai, the Primal Ritualist - Sacred Fumes", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.SACRED_FUMES)
    SKILL_TRAINER_SINAI_NURTURING_ECHO = location("Skill Trainer - Sinai, the Primal Ritualist - Nurturing Echo", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.NURTURING_ECHO)
    SKILL_TRAINER_SINAI_REVERBERATION = location("Skill Trainer - Sinai, the Primal Ritualist - Reverberation", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.REVERBERATION)
    SKILL_TRAINER_SINAI_HARMONY_AND_MELODY = location("Skill Trainer - Sinai, the Primal Ritualist - Harmony and Melody", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.HARMONY_AND_MELODY)
    SKILL_TRAINER_SINAI_BATTLE_RYTHYM = location("Skill Trainer - Sinai, the Primal Ritualist - Battle Rhythym", OutwardRegionName.RITUALISTS_HUT, vanilla_item_name=OutwardItemName.BATTLE_RHYTHM)

    SKILL_TRAINER_STYX_BACKSTAB = location("Skill Trainer - Styx - Backstab", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.BACKSTAB)
    SKILL_TRAINER_STYX_OPPORTUNIST_STAB = location("Skill Trainer - Styx - Opportunist Stab", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.OPPORTUNIST_STAB)
    SKILL_TRAINER_STYX_PRESSURE_PLATE_TRAINING = location("Skill Trainer - Styx - Pressure Plate Training", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.PRESSURE_PLATE_TRAINING)
    SKILL_TRAINER_STYX_SWEEP_KICK = location("Skill Trainer - Styx - Sweep Kick", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.SWEEP_KICK)
    SKILL_TRAINER_STYX_FEATHER_DODGE = location("Skill Trainer - Styx - Feather Dodge", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.FEATHER_DODGE)
    SKILL_TRAINER_STYX_SERPENTS_PARRY = location("Skill Trainer - Styx - Serpent's Parry", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.SERPENTS_PARRY)
    SKILL_TRAINER_STYX_STEALTH_TRAINING = location("Skill Trainer - Styx - Stealth Training", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.STEALTH_TRAINING)
    SKILL_TRAINER_STYX_PRESSURE_PLATE_EXPERTISE = location("Skill Trainer - Styx - Pressure Plate Expertise", OutwardRegionName.LEVANT_SLUMS, vanilla_item_name=OutwardItemName.PRESSURE_PLATE_EXPERTISE)

    SKILL_TRAINER_TURE_ENRAGE = location("Skill Trainer - Ture - Enrage", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.ENRAGE)
    SKILL_TRAINER_TURE_EVASION_SHOT = location("Skill Trainer - Ture - Evasion Shot", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.EVASION_SHOT)
    SKILL_TRAINER_TURE_HUNTERS_EYE = location("Skill Trainer - Ture - Hunter's Eye", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.HUNTERS_EYE)
    SKILL_TRAINER_TURE_SNIPER_SHOT = location("Skill Trainer - Ture - Sniper Shot", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.SNIPER_SHOT)
    SKILL_TRAINER_TURE_SURVIVORS_RESILIENCE = location("Skill Trainer - Ture - Survivor's Resilience", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.SURVIVORS_RESILIENCE)
    SKILL_TRAINER_TURE_PREDATOR_LEAP = location("Skill Trainer - Ture - Predator Leap", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.PREDATOR_LEAP)
    SKILL_TRAINER_TURE_PIERCING_SHOT = location("Skill Trainer - Ture - Piercing Shot", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.PIERCING_SHOT)
    SKILL_TRAINER_TURE_FERAL_STRIKES = location("Skill Trainer - Ture - Feral Strikes", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.FERAL_STRIKES)

    # wind altars

    WIND_ALTAR_CHERSONESE = location("Wind Altar - Chersonese", OutwardRegionName.CHERSONESE_NE, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_CHERSONESE)
    WIND_ALTAR_ENMERKAR_FOREST = location("Wind Altar - Enmerkar Forest", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_ENMERKAR_FOREST)
    WIND_ALTAR_ABRASSAR = location("Wind Altar - Abrassar", OutwardRegionName.ABRASSAR, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_ABRASSAR)
    WIND_ALTAR_HALLOWED_MARSH = location("Wind Altar - Hallowed Marsh", OutwardRegionName.HALLOWED_MARSH, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_HALLOWED_MARSH)
    WIND_ALTAR_ANTIQUE_PLATEAU = location("Wind Altar - Antique Plateau", OutwardRegionName.ANTIQUE_PLATEAU, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_ANTIQUE_PLATEAU)
    WIND_ALTAR_CALDERA = location("Wind Altar - Caldera", OutwardRegionName.CALDERA, vanilla_item_name=OutwardItemName.WIND_ALTAR_BOON_CALDERA)

    # skill trainer interactions
    
    SKILL_TRAINER_INTERACT_ADALBERT = location("Skill Trainer - Interact - Adalbert the Hermit", OutwardRegionName.HERMITS_HOUSE)
    SKILL_TRAINER_INTERACT_ALEMMON = location("Skill Trainer - Interact - Alemmon", OutwardRegionName.MONSOON)
    SKILL_TRAINER_INTERACT_ELLA = location("Skill Trainer - Interact - Ella Lockwell", OutwardRegionName.HARMATTAN)
    SKILL_TRAINER_INTERACT_ETO = location("Skill Trainer - Interact - Eto", OutwardRegionName.CIERZO)
    SKILL_TRAINER_INTERACT_FLASE = location("Skill Trainer - Interact - Flase, Sage Trainer", OutwardRegionName.BERG)
    SKILL_TRAINER_INTERACT_GALIRA = location("Skill Trainer - Interact - Galira", OutwardRegionName.MONSOON)
    SKILL_TRAINER_INTERACT_JAIMON = location("Skill Trainer - Interact - Jaimon", OutwardRegionName.LEVANT)
    SKILL_TRAINER_INTERACT_SERGE = location("Skill Trainer - Interact - Serge Battleborn", OutwardRegionName.HARMATTAN)
    SKILL_TRAINER_INTERACT_SINAI = location("Skill Trainer - Interact - Sinai, the Primal Ritualist", OutwardRegionName.RITUALISTS_HUT)
    SKILL_TRAINER_INTERACT_STYX = location("Skill Trainer - Interact - Styx", OutwardRegionName.LEVANT_SLUMS)
    SKILL_TRAINER_INTERACT_TURE = location("Skill Trainer - Interact - Ture", OutwardRegionName.BERG)

    # friendly immaculate

    FRIENDLY_IMMACULATE_CHERSONESE = location("Friendly Immaculate Gift - Chersonese", OutwardRegionName.IMMACULATES_CAMP_CHERSONESE, vanilla_item_name=OutwardItemName.CALYGREY_BONE_CAGE)
    FRIENDLY_IMMACULATE_ENMERKAR_FOREST = location("Friendly Immaculate Gift - Enmerkar Forest", OutwardRegionName.IMMACULATES_CAMP_ENMERKAR_FOREST, vanilla_item_name=OutwardItemName.BARRIER_ARMOR)
    FRIENDLY_IMMACULATE_ABRASSAR = location("Friendly Immaculate Gift - Abrassar", OutwardRegionName.IMMACULATES_CAMP_CHERSONESE, vanilla_item_name=OutwardItemName.BRIGANDS_BACKPACK)
    FRIENDLY_IMMACULATE_HALLOWED_MARSH = location("Friendly Immaculate Gift - Hallowed Marsh", OutwardRegionName.IMMACULATES_CAMP_HALLOWED_MARSH, vanilla_item_name=OutwardItemName.SCOURGE_COCOON)
    FRIENDLY_IMMACULATE_ANTIQUE_PLATEAU = location("Friendly Immaculate Gift - Antique Plateau", OutwardRegionName.IMMACULATES_CAMP_ANTIQUE_PLATEAU, vanilla_item_name=OutwardItemName.VAGABONDS_GELATIN)
    FRIENDLY_IMMACULATE_CALDERA = location("Friendly Immaculate Gift - Caldera", OutwardRegionName.IMMACULATES_CAMP_CALDERA, vanilla_item_name=OutwardItemName.POSSESSED)

    # blue chamber

    BALTHAZAR_NOTE = location("Spawn - Balthazar's Note", OutwardRegionName.ANCESTORS_RESTING_PLACE_ENMERKAR_FOREST_SIDE, vanilla_item_name=OutwardItemName.BALTHAZAR_NOTE)
    ROLAND_GIFT_1 = location("Roland Argenson - Gift - Cierzo Docks", OutwardRegionName.CIERZO, vanilla_item_name=OutwardItemName.ROLAND_GIFT_1)
    ROLAND_GIFT_2 = location("Roland Argenson - Gift - Berg - Quest 1", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.ROLAND_GIFT_2)
    ROLAND_GIFT_3 = location("Roland Argenson - Gift - Berg - Quest 2", OutwardRegionName.BERG, vanilla_item_name=OutwardItemName.ROLAND_GIFT_3)
    ROLAND_FRIENDSHIP_1 = location("Roland Friendship Event - Cierzo Docks", OutwardRegionName.CIERZO, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)
    ROLAND_FRIENDSHIP_2 = location("Roland Friendship Event - Berg - Quest 1", OutwardRegionName.BERG, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)
    ROLAND_FRIENDSHIP_3 = location("Roland Friendship Event - Berg - Quest 2", OutwardRegionName.BERG, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)
    ROLAND_FRIENDSHIP_4 = location("Roland Friendship Event - Conflux Chambers", OutwardRegionName.CONFLUX_CHAMBERS, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)
    ROLAND_FRIENDSHIP_5 = location("Roland Friendship Event - Starfish Cave", OutwardRegionName.STARFISH_CAVE, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)
    ROLAND_FRIENDSHIP_6 = location("Roland Friendship Event - Cabal of Wind Temple", OutwardRegionName.CABAL_OF_WIND_TEMPLE, faction=OutwardFaction.BlueChamber, vanilla_item_name=OutwardItemName.ROLAND_FRIENDSHIP)

class OutwardLocationGroup:
    SKILL_TRAINER_INTERACT = [
        OutwardLocationName.SKILL_TRAINER_INTERACT_ADALBERT,
        OutwardLocationName.SKILL_TRAINER_INTERACT_ALEMMON,
        OutwardLocationName.SKILL_TRAINER_INTERACT_ELLA,
        OutwardLocationName.SKILL_TRAINER_INTERACT_ETO,
        OutwardLocationName.SKILL_TRAINER_INTERACT_FLASE,
        OutwardLocationName.SKILL_TRAINER_INTERACT_GALIRA,
        OutwardLocationName.SKILL_TRAINER_INTERACT_JAIMON,
        OutwardLocationName.SKILL_TRAINER_INTERACT_SERGE,
        OutwardLocationName.SKILL_TRAINER_INTERACT_SINAI,
        OutwardLocationName.SKILL_TRAINER_INTERACT_STYX,
        OutwardLocationName.SKILL_TRAINER_INTERACT_TURE,
    ]

def add_world_locations(world: OutwardWorld) -> None:
    r"""
    Add all locations to the world.
    """

    for location_name in get_world_location_names(world):
        world.add_location(location_name)

def get_world_location_names(world: OutwardWorld) -> Iterable[str]:
    r"""
    Get the names of all locations to add to the world.
    """

    yield from OutwardLocationName.get_names()
