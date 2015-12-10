<%@ Control Language="C#" %>
<div id="InsertTableLight" class="reInsertTableLightWrapper" style="display: none;">
    <table cellspacing="0" cellpadding="0" border="0" class="reControlsLayout">
		<caption style="display: none;">It contains the Insert Table light dialog, which has the important properties to insert a table into your document: Columns, Rows, Alignment, Cell Padding, Cell Spacing and Border. In the light dialog you also have a button (All Properties) that allows you to switch from Insert Table dialog to Table Wizard dialog if you decide you want to access all table properties options.</caption>
		<thead  style="display: none;">
			<tr>
				<th scope="col">
					<span>Insert Table Light Dialog's wrapper</span>
				</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td colspan="2" class="reTablePropertyControlCell">
					<div class="lightTable" style="border: 0 none;">
						<table cellpadding="0" cellspacing="0">
							<caption style="display: none;">Table's properties - Columns, Rows, Alignment, Cell Padding, Cell Spacing and Border</caption>
							<thead  style="display: none;">
								<tr>
									<th scope="col">
										<span>Labels - Columns, Rows and Alignment</span>
									</th>
									<th scope="col">
										<span>Columns, Rows and Alignment</span>
									</th>
									<th scope="col">
										<span>Labels - Cell Padding, Cell Spacing and Border</span>
									</th>
									<th scope="col">
										<span>Cell Padding, Cell Spacing and Border</span>
									</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<th scope="row" style="display: none;">
										Columns and Cell Padding
									</th>
									<td>
										<label class="reDialogLabelLight" for="Columns">
											<span class="short">[columns]</span>
										</label>
									</td>
									<td>
										<input type="text" id="Columns" class="rfdIgnore" />
									</td>
									<td>
										<label class="reDialogLabelLight" for="CellPadding">
											<span class="short">[cellpadding]</span>
										</label>
									</td>
									<td>
										<input type="text" id="CellPadding" class="rfdIgnore" />
									</td>
								</tr>
								<tr>
									<th scope="row" style="display: none;">
										Rows and Cell Spacing
									</th>
									<td>
										<label class="reDialogLabelLight" for="Rows">
											<span class="short">[rows]</span>
										</label>
									</td>
									<td>
										<input type="text" id="Rows" class="rfdIgnore" />
									</td>
									<td>
										<label class="reDialogLabelLight" for="CellSpacing">
											<span class="short">[cellspacing]</span>
										</label>
									</td>
									<td>
										<input type="text" id="CellSpacing" class="rfdIgnore" />
									</td>
								</tr>
								<tr>
									<th scope="row" style="display: none;">
										Alignment and Border
									</th>
									<td>
										<label class="reDialogLabelLight" for="AlignmentSelectorTable">
											<span>[alignment]</span>
										</label>
									</td>
									<td>
										<div class="reDialog reToolWrapper">
											<a id="AlignmentSelectorTable" title="AlignmentSelector" class="reTool reSplitButton"
												href="#"><span class="AlignmentSelector">&nbsp;</span><span class="split_arrow">&nbsp;</span></a>
										</div>
									</td>
									<td>
										<label class="reDialogLabelLight" for="BorderWidth">
											<span class="short">[border]</span>
										</label>
									</td>
									<td>
										<input type="text" id="BorderWidth" class="rfdIgnore" />&nbsp;&nbsp;px
									</td>
								</tr>
							</tbody>
						</table>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<table border="0" cellpadding="0" cellspacing="0" class="reConfirmCancelButtonsTblLight">
						<caption style="display: none;">Buttons - All Properties, OK and Cancel</caption>
						<thead  style="display: none;">
							<tr>
								<th scope="col">
									<span>All Properties Button</span>
								</th>
								<th scope="col">
									<span>OK Button</span>
								</th>
								<th scope="col">
									<span>Cancel Button</span>
								</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td class="reAllPropertiesLight" style="padding-left: 3px;">
									<button type="button" id="itlAllProperties">
										[allproperties]
									</button>
								</td>
								<td>
									<button type="button" id="itlInsertBtn">
										[ok]
									</button>
								</td>
								<td>
									<button type="button" id="itlCancelBtn">
										[cancel]
									</button>
								</td>
							</tr>
						</tbody>
					</table>
				</td>
			</tr>
		</tbody>
    </table>
</div>
