<%@ Control Language="C#" %>
<div id="InsertImage" class="reInsertImageWrapper" style="display: none;">
	<table cellspacing="0" cellpadding="0" border="0" class="reControlsLayout">
		<caption style="display: none;">It contains the Insert Image light dialog, which has the important properties to put an image into your document: Image Source, Alt Text, Width and Height. In the light dialog you also have a button (All Properties) that allows you to switch from Insert Image dialog to ImageManager with ImageEditor dialog if you decide you want to access all image properties options.</caption>
		<thead style="display: none;">
			<tr>
				<th scope="col">
					<span>Labels - Image Src and Alt Text</span>
				</th>
				<th scope="col">
					<span>Image Src and Alt Text</span>
				</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<th scope="row" style="display: none;">
					Image Src
				</th>
				<td style="vertical-align: middle;">
					<label class="reDialogLabelLight" for="ImageSrc">
						<span>[imagesrc]</span>
					</label>
				</td>
				<td class="reControlCellLight">
					<input type="text" id="ImageSrc" class="rfdIgnore" />
				</td>
			</tr>
			<tr>
				<th scope="row" style="display: none;">
					Alt Text
				</th>
				<td>
					<label class="reDialogLabelLight" for="ImageAlt">
						<span>[imagealttext]</span>
					</label>
				</td>
				<td class="reControlCellLight">
					<input type="text" id="ImageAlt" class="rfdIgnore" />
				</td>
			</tr>
			<tr>
				<td colspan="2" class="reImgPropertyControlCell">
					<table cellpadding="0" cellspacing="0">
						<caption style="display: none;">Here you can set the table's Width and Height.</caption>
						<thead style="display: none;">
							<tr>
								<th scope="col">
									<span>Label - Width</span>
								</th>
								<th scope="col">
									<span>Width</span>
								</th>
								<th scope="col">
									<span>Label - Height</span>
								</th>
								<th scope="col">
									<span>Height</span>
								</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td>
									<label class="reDialogLabelLight" for="ImageWidth">
										<span>[width]</span>
									</label>
								</td>
								<td>
									<input type="text" id="ImageWidth" class="rfdIgnore" />&nbsp;&nbsp;px
								</td>
								<td>
									<label class="reDialogLabelLight" for="ImageHeight">
										<span>[height]</span>
									</label>
								</td>
								<td>
									<input type="text" id="ImageHeight" class="rfdIgnore" />&nbsp;&nbsp;px
								</td>
							</tr>
						</tbody>
					</table>
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
								<td class="reAllPropertiesLight" style="padding-left:3px;">
									<button type="button" id="iplAllProperties">
										[allproperties]
									</button>
								</td>
								<td>
									<button type="button" id="iplInsertBtn">
										[ok]
									</button>
								</td>
								<td>
									<button type="button" id="iplCancelBtn">
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
