<%@ Control Language="C#" %>
<div id="InsertLink" class="reInsertLinkWrapper" style="display: none;">
	<table border="0" cellpadding="0" cellspacing="0" class="reControlsLayout">
		<caption style="display: none;">It contains the Insert Link light dialog, which has the important properties to put a hyperlink in your document: URL, Link Text and Target. In the light dialog you also have a button (All Properties) that allows you to switch from Insert Link dialog to Hyperlink Manager dialog if you need to access all hyperlink options.</caption>
		<thead  style="display: none;">
			<tr>
				<th scope="col">
					<span>Labels - URL, Link Text and Target</span>
				</th>
				<th scope="col">
					<span>URL, Link Text and Target</span>
				</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<th scope="row" style="display: none;">
					URL
				</th>
				<td>
					<label for="LinkURL" class="reDialogLabelLight">
						<span>[linkurl]</span>
					</label>
				</td>
				<td class="reControlCellLight">
					<input type="text" id="LinkUrl" class="rfdIgnore" />
				</td>
			</tr>
			<tr id="texTextBoxParentNode">
				<th scope="row" style="display: none;">
					Link Text
				</th>
				<td>
					<label for="LinkText" class="reDialogLabelLight">
						<span>[linktext]</span>
					</label>
				</td>
				<td class="reControlCellLight">
					<input type="text" id="LinkText" class="rfdIgnore" />
				</td>
			</tr>
			<tr>
				<th scope="row" style="display: none;">
					Target
				</th>
				<td>
					<label for="LinkTargetCombo" class="reDialogLabelLight">
						<span>[linktarget]</span>
					</label>
				</td>
				<td class="reControlCellLight">
					<select id="LinkTargetCombo" class="rfdIgnore">
						<optgroup label="PresetTargets">
							<option value="_none">[none]</option>
							<option value="_self">[targetself]</option>
							<option value="_blank">[targetblank]</option>
							<option value="_parent">[targetparent]</option>
							<option value="_top">[targettop]</option>
							<option value="_search">[targetsearch]</option>
							<option value="_media">[targetmedia]</option>
						</optgroup>
						<optgroup label="CustomTargets">
							<option value="_custom">[addcustomtarget]</option>
						</optgroup>
					</select>
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
								<td class="reAllPropertiesLight">
									<button type="button" id="lmlAllProperties">
									[allproperties]
									</button>
								</td>
								<td>
									<button type="button" id="lmlInsertBtn">
									[ok]
									</button>
								</td>
								<td>
									<button type="button" id="lmlCancelBtn">
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
