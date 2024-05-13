package pt.isel.markettracker.ui.screens.products.filters.facets

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import pt.isel.markettracker.domain.model.market.inventory.product.filter.FacetItem

@Composable
fun <T> FacetRow(facet: FacetItem<T>, title: String, enabled: Boolean, onClick: (T) -> Unit) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .clickable { if(enabled) onClick(facet.item) }
            .padding(vertical = 8.dp),
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.spacedBy(8.dp),
    ) {
        Text(
            text = title,
            fontWeight = if (facet.isSelected) FontWeight.SemiBold else null,
            textAlign = TextAlign.Start
        )

        Spacer(modifier = Modifier.weight(1F))

        Text(
            text = facet.count.toString(),
            color = Color.Gray,
            textAlign = TextAlign.End
        )
    }
}