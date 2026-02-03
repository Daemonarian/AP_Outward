from dataclasses import dataclass
from Options import DeathLink, PerGameCommonOptions

@dataclass
class OutwardOptions(PerGameCommonOptions):
    death_link: DeathLink

