from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Location
from worlds.generic.Rules import add_item_rule, add_rule

from .common import OUTWARD
from .templates import OutwardGameObjectNamespace, OutwardGameObjectTemplate
from .regions import OutwardRegionName

if TYPE_CHECKING:
    from worlds.generic.Rules import CollectionRule, ItemRule

    from . import OutwardWorld

# classes for generating items in the multi-world

class OutwardLocation(Location):
    game = OUTWARD

    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        add_rule(self, rule, combine)

    def add_item_rule(self, rule: ItemRule, combine: str = "and") -> None:
        add_item_rule(self, rule, combine)

class OutwardGameLocation(OutwardLocation):
    def __init__(self, name: str, player: int):
        template = OutwardGameLocationTemplate.get_template(name)
        super().__init__(player, template.name, template.archipelago_id)

    def add_to_world(self, world: OutwardWorld) -> None:
        template = OutwardGameLocationTemplate.get_template(self.name)
        parent = world.get_region(template.region)
        self.parent_region = parent
        parent.locations.append(self)
        
# classes for defining the basic properties of game locations

class OutwardGameLocationTemplate(OutwardGameObjectTemplate):
    _region: str
    def __init__(self, name: str, region: str, archipelago_id: int = -1):
        super().__init__(name, archipelago_id)
        self._region = region
    @property
    def region(self) -> str:
        return self._region

# define the names of Outward locations

location = OutwardGameLocationTemplate.register_template
class OutwardLocationName(OutwardGameObjectNamespace):
    template = OutwardGameLocationTemplate

    # quest completion checks

    QUEST_MAIN_01 = location("Main Quest 1 - Castaways", OutwardRegionName.UNPLACED)
    QUEST_MAIN_02 = location("Main Quest 2 - Call to Adventure", OutwardRegionName.UNPLACED)
    QUEST_MAIN_03 = location("Main Quest 3 - Join Faction Quest", OutwardRegionName.UNPLACED)
    QUEST_MAIN_04 = location("Main Quest 4 - Faction Quest 1", OutwardRegionName.UNPLACED)
    QUEST_MAIN_05 = location("Main Quest 5 - Faction Quest 2", OutwardRegionName.UNPLACED)
    QUEST_MAIN_06 = location("Main Quest 6 - Faction Quest 3", OutwardRegionName.UNPLACED)
    QUEST_MAIN_07 = location("Main Quest 7 - Faction Quest 4", OutwardRegionName.UNPLACED)
    QUEST_MAIN_08 = location("Main Quest 8 - A Fallen City", OutwardRegionName.CALDERA)
    QUEST_MAIN_09 = location("Main Quest 9 - Up The Ladder", OutwardRegionName.CALDERA)
    QUEST_MAIN_10 = location("Main Quest 10 - Stealing Fire", OutwardRegionName.CALDERA)
    QUEST_MAIN_11 = location("Main Quest 11 - Liberate the Sun", OutwardRegionName.CALDERA)
    QUEST_MAIN_12 = location("Main Quest 12 - Vengeful Ouroboros", OutwardRegionName.CALDERA)
    
    QUEST_PARALLEL_BLOOD_UNDER_THE_SUN = location("Parallel Quest - Blood Under the Sun", OutwardRegionName.UNPLACED)
    QUEST_PARALLEL_PURIFIER = location("Parallel Quest - Purifier", OutwardRegionName.UNPLACED)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_1 = location("Parallel Quest - Rust and Vengeance - Rust Lich Armor", OutwardRegionName.UNPLACED)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_2 = location("Parallel Quest - Rust and Vengeance - Rust Lich Boots", OutwardRegionName.UNPLACED)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_3 = location("Parallel Quest - Rust and Vengeance - Rust Lich Helmet", OutwardRegionName.UNPLACED)
    QUEST_PARALLEL_VENDAVEL_QUEST = location("Parallel Quest - Vendavel Quest", OutwardRegionName.UNPLACED)

    QUEST_MINOR_ACQUIRE_MANA = location("Minor Quest - Acquire Mana", OutwardRegionName.UNPLACED)
    QUEST_MINOR_ALCHEMY_COLD_STONE = location("Minor Quest - Alchemy: Cold Stone", OutwardRegionName.UNPLACED)
    QUEST_MINOR_ALCHEMY_CRYSTAL_POWDER = location("Minor Quest - Alchemy: Crystal Powder", OutwardRegionName.UNPLACED)
    QUEST_MINOR_ARCANE_MACHINE = location("Minor Quest - Arcane Machine", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BARREL_MAN = location("Minor Quest - Barrel Man", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_1 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Spear", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_2 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Mask", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_3 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Boots", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_4 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_1 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Staff", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_2 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Boots", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_3 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Mask", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_4 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Robes", OutwardRegionName.UNPLACED)
    QUEST_MINOR_BLOODY_BUSINESS = location("Minor Quest - Bloody Business", OutwardRegionName.UNPLACED)
    QUEST_MINOR_CRAFT_ANTIQUE_PLATE_GARB_ARMOR = location("Minor Quest - Craft: Antique Plate Garb Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_CRAFT_BLUE_SAND_ARMOR = location("Minor Quest - Craft: Blue Sand Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_CRAFT_COPAL_AND_PETRIFIED_ARMOR = location("Minor Quest - Craft: Copal & Petrified Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_CRAFT_PALLADIUM_ARMOR = location("Minor Quest - Craft: Palladium Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_CRAFT_TSAR_AND_TENEBROUS_ARMOR = location("Minor Quest - Craft: Tsar & Tenebrous Armor", OutwardRegionName.UNPLACED)
    QUEST_MINOR_HELENS_FUNGUS = location("Minor Quest - Helen's Fungus", OutwardRegionName.UNPLACED)
    QUEST_MINOR_LEDGER_TO_BERG = location("Minor Quest - Ledger to Berg", OutwardRegionName.UNPLACED)
    QUEST_MINOR_LEDGER_TO_CIERZO = location("Minor Quest - Ledger to Cierzo", OutwardRegionName.UNPLACED)
    QUEST_MINOR_LEDGER_TO_LEVANT = location("Minor Quest - Ledger to Levant", OutwardRegionName.UNPLACED)
    QUEST_MINOR_LEDGER_TO_MONSOON = location("Minor Quest - Ledger to Monsoon", OutwardRegionName.UNPLACED)
    QUEST_MINOR_LOST_MERCHANT = location("Minor Quest - Lost Merchant", OutwardRegionName.UNPLACED)
    QUEST_MINOR_MYRIAD_OF_BONES = location("Minor Quest - A Myriad of Bones", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_ANGEL_FOOD_CAKE = location("Minor Quest - Need: Angel Food Cake", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_BEAST_GOLEM_SCRAPS = location("Minor Quest - Need: Beast Golem Scraps", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_CIERZO_CEVICHE = location("Minor Quest - Need: Cierzo Ceviche", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_FIRE_ELEMENTAL_PARTICLES = location("Minor Quest - Need: Fire Elemental Particles", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_MANAHEART_BASS = location("Minor Quest - Need: Manaheart Bass", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_MANTICORE_TAIL = location("Minor Quest - Need: Manticore Tail", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_SHARK_CARTILAGE = location("Minor Quest - Need: Shark Cartilage", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_SHIELD_GOLEM_SCRAP = location("Minor Quest - Need: Shield Golem Scrap", OutwardRegionName.UNPLACED)
    QUEST_MINOR_NEED_TOURMALINE = location("Minor Quest - Need: Tourmaline", OutwardRegionName.UNPLACED)
    QUEST_MINOR_PURIFY_THE_WATER = location("Minor Quest - Purify the Water", OutwardRegionName.UNPLACED)
    QUEST_MINOR_RED_IDOL = location("Minor Quest - Red Idol", OutwardRegionName.UNPLACED)
    QUEST_MINOR_SCHOLARS_RANSOM = location("Minor Quest - A Scholar's Ransom", OutwardRegionName.UNPLACED)
    QUEST_MINOR_SILVER_FOR_THE_SLUMS = location("Minor Quest - Silver for the Slums", OutwardRegionName.UNPLACED)
    QUEST_MINOR_SKULLS_FOR_CREMEUH = location("Minor Quest - Skulls for Cremeuh", OutwardRegionName.UNPLACED)
    QUEST_MINOR_STRANGE_APPARITIONS = location("Minor Quest - Strange Apparitions", OutwardRegionName.UNPLACED)
    QUEST_MINOR_TREASURE_HUNT = location("Minor Quest - Treasure Hunt", OutwardRegionName.UNPLACED)
    QUEST_MINOR_WILLIAM_OF_THE_WISP = location("Minor Quest - William of the Wisp", OutwardRegionName.UNPLACED)

    # comissions

    COMMISSION_ANTIQUE_PLATE_BOOTS = location("Commission - Antique Plate Boots", OutwardRegionName.UNPLACED)
    COMMISSION_ANTIQUE_PLATE_GARB = location("Commission - Antique Plate Garb", OutwardRegionName.UNPLACED)
    COMMISSION_ANTIQUE_PLATE_SALLET = location("Commission - Antique Plate Sallet", OutwardRegionName.UNPLACED)

    COMMISSION_BLUE_SAND_ARMOR = location("Commission - Blue Sand Armor", OutwardRegionName.UNPLACED)
    COMMISSION_BLUE_SAND_BOOTS = location("Commission - Blue Sand Boots", OutwardRegionName.UNPLACED)
    COMMISSION_BLUE_SAND_HELM = location("Commission - Blue Sand Helm", OutwardRegionName.UNPLACED)

    COMMISSION_COPAL_ARMOR = location("Commission - Copal Armor", OutwardRegionName.UNPLACED)
    COMMISSION_COPAL_BOOTS = location("Commission - Copal Boots", OutwardRegionName.UNPLACED)
    COMMISSION_COPAL_HELM = location("Commission - Copal Helm", OutwardRegionName.UNPLACED)

    COMMISSION_PALLADIUM_ARMOR = location("Commission - Palladium Armor", OutwardRegionName.UNPLACED)
    COMMISSION_PALLADIUM_BOOTS = location("Commission - Palladium Boots", OutwardRegionName.UNPLACED)
    COMMISSION_PALLADIUM_HELM = location("Commission - Palladium Helm", OutwardRegionName.UNPLACED)

    COMMISSION_PETRIFIED_WOOD_ARMOR = location("Commission - Petrified Wood Armor", OutwardRegionName.UNPLACED)
    COMMISSION_PETRIFIED_WOOD_BOOTS = location("Commission - Petrified Wood Boots", OutwardRegionName.UNPLACED)
    COMMISSION_PETRIFIED_WOOD_HELM = location("Commission - Petrified Wood Helm", OutwardRegionName.UNPLACED)

    COMMISSION_TENEBROUS_ARMOR = location("Commission - Tenebrous Armor", OutwardRegionName.UNPLACED)
    COMMISSION_TENEBROUS_BOOTS = location("Commission - Tenebrous Boots", OutwardRegionName.UNPLACED)
    COMMISSION_TENEBROUS_HELM = location("Commission - Tenebrous Helm", OutwardRegionName.UNPLACED)

    COMMISSION_TSAR_ARMOR = location("Commission - Tsar Armor", OutwardRegionName.UNPLACED)
    COMMISSION_TSAR_BOOTS = location("Commission - Tsar Boots", OutwardRegionName.UNPLACED)
    COMMISSION_TSAR_HELM = location("Commission - Tsar Helm", OutwardRegionName.UNPLACED)

    # unique items

    SPAWN_ANGLER_SHIELD = location("Item Upgrade - Caldera - Slumbering Shield", OutwardRegionName.UNPLACED)
    SPAWN_BRAND = location("Item Upgrade - Strange Rusted Sword", OutwardRegionName.UNPLACED)
    SPAWN_BRASS_WOLF_BACKPACK = location("Spawn - Brass-Wolf Backpack", OutwardRegionName.UNPLACED)
    SPAWN_CEREMONIAL_BOW = location("Spawn - Ceremonial Bow", OutwardRegionName.CALDERA)
    SPAWN_COMPASSWOOD_STAFF = location("The Walled Garden - Guardian of the Compass - Compasswood Staff", OutwardRegionName.UNPLACED)
    SPAWN_CRACKED_RED_MOON = location("Scarlet Sanctuary - Crimson Avatar - Cracked Red Moon", OutwardRegionName.UNPLACED)
    SPAWN_DEPOWERED_BLUDGEON = location("Spawn - De-powered Bludgeon", OutwardRegionName.CALDERA)
    SPAWN_DISTORTED_EXPERIMENT = location("Item Upgrade - Caldera - Experimental Chakram", OutwardRegionName.UNPLACED)
    SPAWN_DREAMER_HALBERD = location("Friendly Immaculate Gift - Dreamer Halberd", OutwardRegionName.UNPLACED)
    SPAWN_DUTY = location("Item Upgrade - Silkworm's Refuge - Ruined Halberd", OutwardRegionName.UNPLACED)
    SPAWN_EXPERIMENTAL_CHAKRAM = location("Spawn - Experimental Chakram", OutwardRegionName.CALDERA)
    SPAWN_FABULOUS_PALLADIUM_SHIELD = location("Spawn - Fabulous Palladium Shield", OutwardRegionName.UNPLACED)
    SPAWN_FOSSILIZED_GREATAXE = location("Steam Bath Tunnels - Calygrey Hero - Fossilized Greataxe", OutwardRegionName.CALDERA)
    SPAWN_GEPS_BLADE = location("Item Upgrade - Mysterious Blade/Long Blade", OutwardRegionName.UNPLACED)
    SPAWN_GHOST_PARALLEL = location("Item Upgrade - De-Powered Bludgeon", OutwardRegionName.UNPLACED)
    SPAWN_GILDED_SHIVER_OF_TRAMONTANE = location("Item Upgrade - Scarred Dagger", OutwardRegionName.UNPLACED)
    SPAWN_GRIND = location("Item Upgrade - Oily Cavern - Fossilized Greataxe", OutwardRegionName.UNPLACED)
    SPAWN_KRYPTEIA_TOMB_KEY = location("Face of the Ancients - The First Cannibal - Krypteia Tomb Key", OutwardRegionName.UNPLACED)
    SPAWN_LIGHT_MENDERS_LEXICON = location("Spawn - Light Mender's Lexicon", OutwardRegionName.UNPLACED)
    SPAWN_MEFINOS_TRADE_BACKPACK = location("Spawn - Mefino's Trade Backpack", OutwardRegionName.UNPLACED)
    SPAWN_MERTONS_FIREPOKER = location("Spawn - Merton's Firepoker", OutwardRegionName.UNPLACED)
    SPAWN_MERTONS_RIBCAGE = location("Spawn - Merton's Ribcage", OutwardRegionName.UNPLACED)
    SPAWN_MERTONS_SHINBONES = location("Spawn - Merton's Shinbones", OutwardRegionName.UNPLACED)
    SPAWN_MERTONS_SKULL = location("Spawn - Merton's Skull", OutwardRegionName.UNPLACED)
    SPAWN_MURMURE = location("Item Upgrade - Ceremonial Bow", OutwardRegionName.UNPLACED)
    SPAWN_MYSTERIOUS_LONG_BLADE = location("Spawn - Mysterious Long Blade", OutwardRegionName.CALDERA)
    SPAWN_PEARLESCENT_MAIL = location("Ruins of Old Levant - Luke the Pearlescent - Pearlescent Mail", OutwardRegionName.UNPLACED)
    SPAWN_PILLAR_GREATHAMMER = location("Spawn - Pillar Greathammer", OutwardRegionName.UNPLACED)
    SPAWN_PORCELAIN_FISTS = location("Crumbling Loading Docks - Giant Horror - Porcelain Fists", OutwardRegionName.UNPLACED)
    SPAWN_RED_LADYS_DAGGER = location("Face of the Ancients - Red Lady's Altar - Krypteia Tomb Key", OutwardRegionName.UNPLACED)
    SPAWN_REVENANT_MOON = location("Item Upgrade - Cracked Red Moon", OutwardRegionName.UNPLACED)
    SPAWN_ROTWOOD_STAFF = location("Item Upgrade - Necroplis - Compasswood Staff", OutwardRegionName.UNPLACED)
    SPAWN_RUINED_HALBERD = location("Spawn - Ruined Halberd", OutwardRegionName.CALDERA)
    SPAWN_RUSTED_SPEAR = location("Spawn - Rusted Spear", OutwardRegionName.CALDERA)
    SPAWN_SANDROSE = location("Item Upgrade - Eldest Brother - Warm Axe", OutwardRegionName.UNPLACED)
    SPAWN_SCARLET_BOOTS = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Boots", OutwardRegionName.UNPLACED)
    SPAWN_SCARLET_GEM = location("Spawn - Scarlet Gem", OutwardRegionName.UNPLACED)
    SPAWN_SCARLET_LICHS_IDOL = location("Spawn - Scarlet Lich's Idol", OutwardRegionName.UNPLACED)
    SPAWN_SCARLET_MASK = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Mask", OutwardRegionName.UNPLACED)
    SPAWN_SCARLET_ROBES = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Robes", OutwardRegionName.UNPLACED)
    SPAWN_SCARRED_DAGGER = location("Spawn - Scarred Dagger", OutwardRegionName.UNPLACED)
    SPAWN_SCEPTER_OF_THE_CRUEL_PRIEST = location("Item Upgrade - Giant's Sauna - Sealed Mace", OutwardRegionName.UNPLACED)
    SPAWN_SEALED_MACE = location("Old Sirocco - Smelly Sealed Box interaction with forge", OutwardRegionName.UNPLACED)
    SPAWN_SHRIEK = location("Item Upgrade - Sulphuric Caverns - Rusted Spear", OutwardRegionName.UNPLACED)
    SPAWN_SKYCROWN_MACE = location("Face of the Ancients - The First Cannibal - Skycrown Mace", OutwardRegionName.UNPLACED)
    SPAWN_SLUMBERING_SHIELD = location("Spawn - Slumbering Shield", OutwardRegionName.CALDERA)
    SPAWN_SMELLY_SEALED_BOX = location("Spawn - Smelly Sealed Box", OutwardRegionName.CALDERA)
    SPAWN_STARCHILD_CLAYMORE = location("Enmerkar Forest - Royal Manticore - Starchild Claymore", OutwardRegionName.UNPLACED)
    SPAWN_STRANGE_RUSTED_SWORD = location("Spawn - Strange Rusted Sword", OutwardRegionName.UNPLACED)
    SPAWN_SUNFALL_AXE = location("Spawn - Sunfall Axe", OutwardRegionName.UNPLACED)
    SPAWN_THRICE_WROUGHT_HALBERD = location("Spawn - Thrice-Wrought Halberd", OutwardRegionName.UNPLACED)
    SPAWN_TOKEBAKICIT = location("Item Upgrade - Unusual Knuckles", OutwardRegionName.UNPLACED)
    SPAWN_TSAR_FISTS = location("Spawn - Tsar Fists", OutwardRegionName.UNPLACED)
    SPAWN_UNUSUAL_KNUCKLES = location("Spawn - Unusual Knuckles", OutwardRegionName.UNPLACED)
    SPAWN_WARM_AXE = location("Spawn - Warm Axe", OutwardRegionName.UNPLACED)
    SPAWN_WERLIG_SPEAR = location("Spawn - Werlig Spear", OutwardRegionName.UNPLACED)
    SPAWN_WORLDEDGE_GREATAXE = location("Enmerkar Forest - Tyrant of the Hive - Worldedge Greataxe", OutwardRegionName.UNPLACED)
    SPAWN_ZHORNS_DEMON_SHIELD = location("Spawn - Zhorn's Demon Shield", OutwardRegionName.UNPLACED)
    SPAWN_ZHORNS_GLOWSTONE_DAGGER = location("Spawn - Zhorn's Glowstone Dagger", OutwardRegionName.UNPLACED)
    SPAWN_ZHORNS_HUNTING_BACKPACK = location("Spawn - Zhorn's Hunting Backpack", OutwardRegionName.UNPLACED)
    SPAWN_MYRMITAUR_HAVEN_GATE_KEY = location("Myrmitaur Haven - Matriarch Myrmitaur - Myrmitaur Haven Gate Key", OutwardRegionName.CALDERA)

    # individual skills learned

    BURAC_FREE_SKILL = location("Burac - Free Skill", OutwardRegionName.CIERZO)
    WATCHER_FREE_SKILL = location("Watcher - Free Skill", OutwardRegionName.LEYLINE_ANY)
    TRAIN_VENDAVEL_PRISONER = location("Train - Vendavel Prisoner", OutwardRegionName.VENDAVEL_FORTRESS)
    TRAIN_FIRST_WATCHER = location("Train - First Watcher", OutwardRegionName.CONFLUX_CHAMBERS)
    TRAIN_TALERON = location("Train - Taleron", OutwardRegionName.BERG)
    TRAIN_MARKUS = location("Train - Markus", OutwardRegionName.LEVANT_SLUMS)
    TRAIN_SAGARD_BATTLEBORN = location("Train - Sagard Battleborn", OutwardRegionName.BERG)
    TRAIN_SOERAN = location("Train - Soeran", OutwardRegionName.MONSOON)
    TRAIN_WANDERING_MERCENARY = location("Train - Wandering Mercenary", OutwardRegionName.MONSOON)
    TRAIN_KING_SIMEON = location("Train - King Simeon", OutwardRegionName.LEVANT)
    TRAIN_BURAC = location("Train - Burac", OutwardRegionName.CIERZO)
    TRAIN_ODA = location("Train - Oda", OutwardRegionName.CIERZO)
    TRAIN_SAMANTHA_TURNBULL = location("Train - Samantha Turnbull", OutwardRegionName.NEW_SIROCCO)
    TRAIN_ANTHONY_BERTHELOT = location("Train - Anthony Berthelot", OutwardRegionName.NEW_SIROCCO)
    TRAIN_SMOOTH = location("Train - Smooth", OutwardRegionName.LEVANT_SLUMS)
    TRAIN_SECOND_WATCHER = location("Train - Second Watcher", OutwardRegionName.CONFLUX_CHAMBERS)
    TRAIN_CYRIL_TURNBULL = location("Train - Cyril Turnbull", OutwardRegionName.BERG)
    TRAIN_MOFAT = location("Train - Mofat", OutwardRegionName.MONSOON)
    TRAIN_NINTH_WATCHER = location("Train - The 9th Watcher", OutwardRegionName.HARMATTAN)
    TRAIN_SKELETON = location("Train - Skeleton", OutwardRegionName.WENDIGO_LAIR)
    TRAIN_RAUL_SALABERRY = location("Train - Headmaster Raul Salaberry", OutwardRegionName.HARMATTAN)
    TRAIN_ROBYN_GARNET = location("Train - Robyn Garnet, Alchemist", OutwardRegionName.HARMATTAN)
    TRAIN_MAVITH = location("Train - Mavith", OutwardRegionName.HARMATTAN)
    TRAIN_PAUL = location("Train - Paul, Disciple", OutwardRegionName.NEW_SIROCCO)
    TRAIN_YAN = location("Train - Yan, Levantine Alchemist", OutwardRegionName.NEW_SIROCCO)
    REPAIR_HORSE_STATUE = location("Repair Horse Statue", OutwardRegionName.NEW_SIROCCO)
    SKILL_BLADE_PUPPY = location("Drop - Plague Doctor - Blade Puppy", OutwardRegionName.DARK_ZIGGURAT)
    SKILL_GOLDEN_WATCHER = location("Drop - Light Mender - Golden Watcher", OutwardRegionName.SPIRE_OF_LIGHT)
