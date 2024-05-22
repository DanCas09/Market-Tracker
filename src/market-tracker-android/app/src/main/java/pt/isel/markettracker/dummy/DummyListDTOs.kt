package pt.isel.markettracker.dummy

import pt.isel.markettracker.domain.model.market.list.ListInfo
import java.time.LocalDateTime
import java.util.UUID

val dummyList = mutableListOf(
    ListInfo(
        1,
        "Festa de aniversário",
        LocalDateTime.now().minusDays(1),
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        0,
        isOwner = true
    ),
    ListInfo(
        2,
        "Ano Novo",
        null,
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        1,
        isOwner = true
    ),
    ListInfo(
        3,
        "Compras Semanais",
        null,
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        1,
        isOwner = false
    ),
    ListInfo(
        4,
        "Compras Mensais",
        LocalDateTime.now().minusDays(1),
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        5,
        isOwner = false
    ),
    ListInfo(
        5,
        "Presentes de Natal",
        LocalDateTime.now().minusDays(1),
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        2,
        isOwner = false
    ),
    ListInfo(
        6,
        "Aperitivos para a festa",
        LocalDateTime.now().minusDays(1),
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        7,
        isOwner = false
    ),
    ListInfo(
        7,
        "Coisas que provavelmente não vou comprar",
        LocalDateTime.now().minusDays(1),
        LocalDateTime.now().minusDays(2),
        UUID(1, 1),
        4,
        isOwner = false
    )
)