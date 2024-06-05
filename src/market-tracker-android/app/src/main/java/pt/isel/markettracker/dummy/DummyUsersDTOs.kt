package pt.isel.markettracker.dummy

import pt.isel.markettracker.domain.model.account.Client
import java.time.LocalDateTime

val dummyClients = mutableListOf(
        Client(
            id = "1",
            username = "alberto_concertina",
            email = "alberto@gmail.com",
            avatar = "https://i.imgur.com/1.jpg",
            createdAt = LocalDateTime.now()
        )
    )